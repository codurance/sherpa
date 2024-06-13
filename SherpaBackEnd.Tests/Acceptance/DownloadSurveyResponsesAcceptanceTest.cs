using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Persistence;
using SherpaBackEnd.Tests.Builders;

namespace SherpaBackEnd.Tests.Acceptance;

public class DownloadSurveyResponsesAcceptanceTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _surveyCollection;
    private IMongoCollection<BsonDocument> _templateCollection;

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
            SurveyNotificationCollectionName = "SurveyNotifications",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });

        var mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);

        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
    }

    [Fact]
    public async Task UserShouldBeAbleToGetSurveyResponsesFile()
    {
        await InitializeDbClientAndCollections();
        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveyResponsesFileService = new SurveyResponsesCsvFileService();
        var surveyService = new SurveyService(surveyRepository, teamRepository, templateRepository,
            surveyResponsesFileService);
        var surveyController = new SurveyController(surveyService, _logger);

        IEnumerable<IQuestion> questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 1" },
                    { Languages.ENGLISH, "Question 1" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 1
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 2" },
                    { Languages.ENGLISH, "Question 2" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 2
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 3" },
                    { Languages.ENGLISH, "Question 3" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "Uno", "Dos", "Tres" } },
                    { Languages.ENGLISH, new[] { "One", "Two", "Three" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 3
            ),
        };
        var template = TemplateBuilder.ATemplate().WithName("Hackman Model").WithQuestions(questions)
            .WithMinutesToComplete(30).Build();
        var janeTeamMember = TeamMemberBuilder.ATeamMember().WithId(Guid.NewGuid()).WithFullName("Jane Doe").Build();
        var johnTeamMember = TeamMemberBuilder.ATeamMember().WithId(Guid.NewGuid()).WithFullName("John Doe").Build();

        var janeAnswers = new List<QuestionResponse>()
        {
            new QuestionResponse(1, "1"),
            new QuestionResponse(2, "3"),
            new QuestionResponse(3, "Two"),
        };
        var johnAnswers = new List<QuestionResponse>()
        {
            new QuestionResponse(1, "2"),
            new QuestionResponse(2, "1"),
            new QuestionResponse(3, "Three"),
        };
        var responses = new List<SurveyResponse>()
        {
            new SurveyResponse(janeTeamMember.Id, janeAnswers),
            new SurveyResponse(johnTeamMember.Id, johnAnswers)
        };

        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { janeTeamMember, johnTeamMember })
            .Build();
        var survey = SurveyBuilder.ASurvey().WithId(Guid.NewGuid()).WithTemplate(template).WithResponses(responses)
            .WithTeam(team).Build();

        await teamRepository.AddTeamAsync(team);
        await teamRepository.AddTeamMemberToTeamAsync(team.Id, janeTeamMember);
        await teamRepository.AddTeamMemberToTeamAsync(team.Id, johnTeamMember);
        await surveyRepository.CreateSurvey(survey);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            {
                "questions", new BsonArray()
                {
                    BsonDocument.Parse(JsonConvert.SerializeObject(template.Questions.ElementAt(0))),
                    BsonDocument.Parse(JsonConvert.SerializeObject(template.Questions.ElementAt(1))),
                    BsonDocument.Parse(JsonConvert.SerializeObject(template.Questions.ElementAt(2))),
                }
            }
        });
        
        var expectedCsvContent = "Response,1. Question 1,2. Question 2,3. Question 3\n1,1,3,Two\n2,2,1,Three";
        // WHEN the coach requests survey responses file
        var actionResult = await surveyController.GetSurveyResponsesFile(survey.Id);

        // THEN the controller should return a file with expected content
        var fileResult = Assert.IsType<FileStreamResult>(actionResult);
        Assert.Equal("text/csv", fileResult.ContentType);
        fileResult.FileStream.Position = 0;
        using (var streamReader = new StreamReader(fileResult.FileStream))
        {
            var content = await streamReader.ReadToEndAsync();
            Assert.Equal(expectedCsvContent, content);
        }

    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}