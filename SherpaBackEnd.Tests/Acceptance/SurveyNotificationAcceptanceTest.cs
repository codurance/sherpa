using System.Net;
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
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Services;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Responses;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Infrastructure.Persistence;
using SherpaBackEnd.Tests.Builders;

namespace SherpaBackEnd.Tests.Acceptance;

public class SurveyNotificationAcceptanceTest : IAsyncLifetime
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly ILogger<SurveyNotificationController> _logger =
        new Mock<ILogger<SurveyNotificationController>>().Object;

    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _teamCollection;
    private IMongoCollection<BsonDocument> _teamMemberCollection;
    private IMongoCollection<BsonDocument> _templateCollection;
    private IMongoCollection<BsonDocument> _surveyCollection;

    [Fact]
    public async Task UserShouldBeAbleToGetSurveyNotification()
    {
        var mongoSurveyNotificationRepository = new MongoSurveyNotificationRepository(_databaseSettings);
        var mongoSurveyRepository = new MongoSurveyRepository(_databaseSettings);
        var guidService = new GuidService();
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var emailTemplateFactory = new EmailTemplateFactory(httpContextAccessor.Object);
        var emailService = new Mock<IEmailService>();
        var surveyNotificationService =
            new SurveyNotificationService(mongoSurveyRepository, mongoSurveyNotificationRepository,
                emailTemplateFactory, emailService.Object, guidService);
        var surveyNotifcationsController = new SurveyNotificationController(surveyNotificationService, _logger);

        var template = TemplateBuilder.ATemplate().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();

        var mongoTeamRepository = new MongoTeamRepository(_databaseSettings);
        await mongoTeamRepository.AddTeamAsync(team);
        await mongoTeamRepository.AddTeamMemberToTeamAsync(team.Id, teamMember);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        await mongoSurveyRepository.CreateSurvey(survey);

        // Given a Survey Notification exists
        var surveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(Guid.NewGuid(), survey, teamMember);
        await mongoSurveyNotificationRepository.CreateManySurveyNotification(
            new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>() { surveyNotification });
        var surveyNotificationResponse = SurveyNotificationResponse.FromSurveyNotification(surveyNotification);

        // When a user requests it
        var actionResult = await surveyNotifcationsController.GetSurveyNotification(surveyNotification.Id);

        // Then the controller should return an Ok result
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        CustomAssertions.StringifyEquals(surveyNotificationResponse, okObjectResult.Value);
    }

    public async Task InitializeAsync()
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

        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);

        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }

    public async Task DisposeAsync()
    {
        await _mongoDbContainer.StopAsync();
    }
}