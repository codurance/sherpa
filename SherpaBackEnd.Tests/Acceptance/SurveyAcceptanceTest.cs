using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;
using SherpaBackEnd.Repositories.Mongo;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class SurveyAcceptanceTest
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMongoCollection<BsonDocument> _surveyCollection;
    private readonly IMongoCollection<BsonDocument> _teamCollection;
    private readonly IMongoCollection<BsonDocument> _teamMemberCollection;
    private readonly IMongoCollection<BsonDocument> _templateCollection;
    
    private ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private const string TestFolder = "test/unit";
    private const string QuestionInSpanish = "Question in spanish";
    private const string QuestionInEnglish = "Question in english";
    private const string ResponseSpanish1 = "SPA_1";
    private const string ResponseSpanish2 = "SPA_2";
    private const string ResponseSpanish3 = "SPA_3";
    private const string ResponseEnglish1 = "ENG_1";
    private const string ResponseEnglish2 = "ENG_2";
    private const string ResponseEnglish3 = "ENG_3";
    private const int Position = 1;
    private const bool Reverse = false;

    public SurveyAcceptanceTest()
    {
        _databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });
        
        var mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);

        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);

        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
        
        Directory.CreateDirectory(TestFolder);
        var contents =
            $@"position|responses_english|responses_spanish|question_english|question_spanish|reverse|component|subcategory|subcomponent
{Position}|{ResponseEnglish1} // {ResponseEnglish2} // {ResponseEnglish3}|{ResponseSpanish1} // {ResponseSpanish2} // {ResponseSpanish3}|{QuestionInEnglish}|{QuestionInSpanish}|{Reverse.ToString()}|{HackmanComponent.INTERPERSONAL_PEER_COACHING}|{HackmanSubcategory.DELIMITED}|{HackmanSubcomponent.SENSE_OF_URGENCY}
";
        File.WriteAllText($"{TestFolder}/hackman.csv", contents);
        
        
    }

    [Fact]
    public async Task UserShouldBeAbleToCreateASurveyAndRetrieveItByItsIdLater()
    {
        //Given: A user interacting with the backend API
        var questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, QuestionInSpanish },
                    { Languages.ENGLISH, QuestionInEnglish },
                }, new Dictionary<string, string[]>()
                {
                    {
                        Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                    },
                    {
                        Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                    }
                }, Reverse,
                HackmanComponent.INTERPERSONAL_PEER_COACHING,
                HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position)
        };

        var template = new Template("Hackman Model", questions, 30);
        var inMemoryTemplateRepository = new MongoTemplateRepository(_databaseSettings);
        
        var team = new Team(Guid.NewGuid(), "Team Test");
        var inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);

        var inMemorySurveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveysService = new SurveyService(inMemorySurveyRepository, inMemoryTeamRepository, inMemoryTemplateRepository);
        var surveyController = new SurveyController(surveysService, _logger);
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), team.Id, template.Name, "survey title",
            "Description", DateTime.Parse("2023-08-09T07:38:04+0000"));
        var expectedSurvey = new Survey(createSurveyDto.SurveyId, new User(surveysService.DefaultUserId, "Lucia"), Status.Draft,
            createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, new List<Response>(),
            team, template);

        //When: they create a new Survey 
        var actionResult = await surveyController.CreateSurvey(createSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);
        
        //And: they get it back later
        var retrievedSurvey = await surveyController.GetSurveyById(createSurveyDto.SurveyId);

        //Then: they should retrieve the same survey they have already created
        var okObjectResult = Assert.IsType<OkObjectResult>(retrievedSurvey.Result);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        CustomAssertions.StringifyEquals(expectedSurvey, okObjectResult.Value);
    }
    
    [Fact]
    public async Task ShouldBeAbleToRetrieveATeamSurveys()
    {
        //Given: A user interacting with the backend API

        var inMemoryTemplateRepository = new MongoTemplateRepository(_databaseSettings);
        
        var teamId = Guid.NewGuid();
        var emptySurveyList = new List<Survey>() { };
        var inMemorySurveyRepository = new MongoSurveyRepository(_databaseSettings);
        ITeamRepository inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);
        var surveysService = new SurveyService(inMemorySurveyRepository, inMemoryTeamRepository, inMemoryTemplateRepository);
        
        var surveyController = new SurveyController(surveysService, _logger);

        var teamSurveys = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<OkObjectResult>(teamSurveys.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        Assert.Equal(emptySurveyList, resultObject.Value);
    }
    
    public void Dispose()
    {
        File.Delete($"{TestFolder}/hackman.csv");
        Directory.Delete(TestFolder);
    }
}