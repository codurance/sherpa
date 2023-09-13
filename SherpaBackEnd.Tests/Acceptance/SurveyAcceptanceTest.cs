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
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;
using SherpaBackEnd.Template.Infrastructure.Persistence;

namespace SherpaBackEnd.Tests.Acceptance;

public class SurveyAcceptanceTest: IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _teamCollection;
    private IMongoCollection<BsonDocument> _teamMemberCollection;
    private IMongoCollection<BsonDocument> _templateCollection;
    private IMongoCollection<BsonDocument> _surveyCollection;

    private async Task InitializeDbClientAndCollections()
    {
        await _mongoDbContainer.StartAsync();
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

        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
        
        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }

    [Fact]
    public async Task UserShouldBeAbleToCreateASurveyAndRetrieveItByItsIdLater()
    {
        await InitializeDbClientAndCollections();
        var teamMemberId = Guid.NewGuid();
        var teamMember = new TeamMember(teamMemberId, "Some name", "Some position", "some@email.com");
        var teamId = Guid.NewGuid();
        var team = new Team.Domain.Team(teamId, "Some team name", new List<TeamMember> { teamMember });
        var template = new TemplateWithoutQuestions("Hackman Model", 10);

        await _teamMemberCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", teamMemberId.ToString() },
            { "FullName", teamMember.FullName },
            { "Position", teamMember.Position },
            { "Email", teamMember.Email },
        });

        await _teamCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", teamId.ToString() },
            { "Name", team.Name },
            { "Members", new BsonArray() { teamMemberId.ToString() } },
            { "IsDeleted", team.IsDeleted }
        });

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "questions", new BsonArray() },
            { "minutesToComplete", template.MinutesToComplete }
        });

        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);

        var surveysService = new SurveyService(surveyRepository, teamRepository, templateRepository);

        var surveyController = new SurveyController(surveysService, _logger);

        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), team.Id, template.Name, "survey title",
            "Description", DateTime.Parse("2023-08-09T07:38:04+0000").ToUniversalTime());

        var expectedSurvey = new SurveyWithoutQuestions(createSurveyDto.SurveyId, new User.Domain.User(surveysService.DefaultUserId, "Lucia"),
            SurveyStatus.Draft,
            createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description, new List<SurveyResponse>(),
            team, template);

        //When: they create a new Survey 
        var actionResult = await surveyController.CreateSurvey(createSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);

        //And: they get it back later
        var retrievedSurvey = await surveyController.GetSurveyWithoutQuestionsById(createSurveyDto.SurveyId);

        //Then: they should retrieve the same survey they have already created
        var okObjectResult = Assert.IsType<OkObjectResult>(retrievedSurvey.Result);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        CustomAssertions.StringifyEquals(expectedSurvey, okObjectResult.Value);
    }

    [Fact]
    public async Task ShouldBeAbleToRetrieveATeamSurveys()
    {
        await InitializeDbClientAndCollections();

        var teamMemberId = Guid.NewGuid();
        var teamMember = new TeamMember(teamMemberId, "Some name", "Some position", "some@email.com");
        var teamId = Guid.NewGuid();
        var team = new Team.Domain.Team(teamId, "Some team name", new List<TeamMember> { teamMember });
        var template = new Template.Domain.Template("Hackman Model", new List<IQuestion>(), 10);
        var survey = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Demo coach"), SurveyStatus.Draft, DateTime.Now.ToUniversalTime(),
            "Survey title", "Survey Description", new List<SurveyResponse>(), team, template);

        var survey2 = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Demo coach"), SurveyStatus.Draft, DateTime.Now.ToUniversalTime(),
            "Survey title", "Survey Description", new List<SurveyResponse>(), team, template);

        await _teamMemberCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", teamMemberId.ToString() },
            { "FullName", teamMember.FullName },
            { "Position", teamMember.Position },
            { "Email", teamMember.Email },
        });

        await _teamCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", teamId.ToString() },
            { "Name", team.Name },
            { "Members", new BsonArray() { teamMemberId.ToString() } },
            { "IsDeleted", team.IsDeleted }
        });

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "questions", new BsonArray() },
            { "minutesToComplete", template.MinutesToComplete }
        });

        await _surveyCollection.InsertManyAsync(new List<BsonDocument>()
        {
            new BsonDocument
            {
                { "_id", survey.Id.ToString() },
                {
                    "Coach", new BsonDocument
                    {
                        { "_id", survey.Coach.Id.ToString() },
                        { "Name", survey.Coach.Name }
                    }
                },
                { "Status", survey.SurveyStatus.ToString() },
                { "Title", survey.Title },
                { "Description", survey.Description },
                { "Responses", new BsonArray() },
                { "Team", survey.Team.Id.ToString() },
                { "Template", survey.Template.Name }
            },
            new BsonDocument
            {
                { "_id", survey2.Id.ToString() },
                {
                    "Coach", new BsonDocument
                    {
                        { "_id", survey.Coach.Id.ToString() },
                        { "Name", survey.Coach.Name }
                    }
                },
                { "Status", survey2.SurveyStatus.ToString() },
                { "Title", survey2.Title },
                { "Description", survey2.Description },
                { "Responses", new BsonArray() },
                { "Team", survey2.Team.Id.ToString() },
                { "Template", survey2.Template.Name }
            }
        });

        //Given: A user interacting with the backend API

        var templateRepository = new MongoTemplateRepository(_databaseSettings);

        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        ITeamRepository teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveysService = new SurveyService(surveyRepository, teamRepository, templateRepository);

        var surveyController = new SurveyController(surveysService, _logger);

        var teamSurveys = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<OkObjectResult>(teamSurveys.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var surveys = Assert.IsType<List<Survey.Domain.Survey>>(resultObject.Value);
        Assert.Equal(2, surveys.Count);
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}