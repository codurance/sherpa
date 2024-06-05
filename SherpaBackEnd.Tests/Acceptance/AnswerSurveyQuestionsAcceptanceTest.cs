using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Infrastructure.Persistence;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
using static SherpaBackEnd.Tests.Builders.TeamBuilder;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;
using static SherpaBackEnd.Tests.Builders.TemplateBuilder;

namespace SherpaBackEnd.Tests.Acceptance;

public class AnswerSurveyQuestionsAcceptanceTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
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
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });

        var mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
    }

    [Fact]
    public async Task UserShouldBeAbleToRespondToSurveyAndResponsesShouldBeStoredInDatabase()
    {
        await InitializeDbClientAndCollections();
        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveyService = new SurveyService(surveyRepository, teamRepository, templateRepository);
        var surveyController = new SurveyController(surveyService, _logger);
        var surveyId = Guid.NewGuid();
        var teamMemberId = Guid.NewGuid();
        var teamId = Guid.NewGuid();

        var teamMember = ATeamMember()
            .WithId(teamMemberId)
            .Build();
        var team = ATeam()
            .WithId(teamId)
            .WithTeamMembers(new List<TeamMember>() { teamMember })
            .Build();
        var template = ATemplate()
            .Build();
        var survey = ASurvey()
            .WithId(surveyId)
            .WithTeam(team)
            .WithTemplate(template)
            .Build();

        await teamRepository.AddTeamAsync(team);
        await teamRepository.AddTeamMemberToTeamAsync(team.Id, teamMember);
        await surveyRepository.CreateSurvey(survey);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        // Given: A TeamMember has responded to a survey
        var response = new SurveyResponse(teamMemberId);
        var answerSurveyDto = new AnswerSurveyDto(surveyId, teamMemberId, response);

        // When: The responses are submitted 
        var actionResult = await surveyController.AnswerSurvey(answerSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);

        // Then: The responses should be stored in the survey
        var retrievedSurvey = await surveyController.GetSurveyWithoutQuestionsById(surveyId);
        Assert.NotNull(retrievedSurvey.Value);
        Assert.Contains(response, retrievedSurvey.Value.Responses);
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}