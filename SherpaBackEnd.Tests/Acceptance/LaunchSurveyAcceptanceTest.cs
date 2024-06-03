using System.Net;
using Amazon.SimpleEmail.Model;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Sur;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Domain;

namespace SherpaBackEnd.Tests.Acceptance;

public class LaunchSurveyAcceptanceTest: IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();
    
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
    public async Task ShouldBeAbleToLaunchASurvey()
    { 
        await InitializeDbClientAndCollections();
        // Given that an Org.coach has created a survey
        var emailService = new Mock<IEmailService>();
        var surveyNotificationService = new SurveyNotificationService();
        var launchSurveyController = new LaunchSurveyController(surveyNotificationService);
        
        var launchSurveyDto = new LaunchSurveyDto(Guid.NewGuid());
        // When they launch the survey
        
        var actionResult = await launchSurveyController.LaunchSurvey(launchSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);
        
        // Then an email with a survey link should be sent to each team member
        List<string> recipients = new(){"fulanito@codurance.com"};

        
        // emailService.Verify(service => service.SendTemplatedEmail(templateRequest));
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}