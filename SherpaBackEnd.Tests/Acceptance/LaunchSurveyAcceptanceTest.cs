using System.Net;
using Amazon.SimpleEmail.Model;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SherpaBackEnd.Email;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Services;
using SherpaBackEnd.SurveyNotification;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Domain;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Domain;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
using static SherpaBackEnd.Tests.Builders.TeamBuilder;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;

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

        var mongoTeamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        
        var teamMember = ATeamMember().WithFullName("Jane Doe").WithEmail("jane.doe@codurance.com").Build();
        var teamMembers = new List<TeamMember>() { teamMember };
        var team = ATeam().WithTeamMembers(teamMembers).Build();
        var template = new Template.Domain.Template("Hackman Model", new List<IQuestion>(), 30);
        var surveyId = Guid.NewGuid();
        var survey = ASurvey().WithId(surveyId).WithTeam(team).WithTemplate(template).Build();

        await mongoTeamRepository.AddTeamAsync(team);
        await mongoTeamRepository.AddTeamMemberToTeamAsync(team.Id, teamMember);
        await surveyRepository.CreateSurvey(survey);
        
        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });
        
        // Given that an Org.coach has created a survey
        var emailService = new Mock<IEmailService>();
        var surveyNotificationRepository = new MongoSurveyNotificationRepository(_databaseSettings);
        var guidService = new Mock<IGuidService>();
        var surveyNotificationId = Guid.NewGuid();
        guidService.Setup(service => service.GenerateRandomGuid()).Returns(surveyNotificationId);
        var emailTemplateFactory = new EmailTemplateFactory();
        var surveyNotificationService = new SurveyNotificationService(surveyRepository, surveyNotificationRepository, emailTemplateFactory, guidService.Object);
        var launchSurveyController = new SurveyNotificationController(surveyNotificationService);
        
        var launchSurveyDto = new CreateSurveyNotificationsDto(Guid.NewGuid());
        // When they launch the survey
        
        var actionResult = await launchSurveyController.LaunchSurvey(launchSurveyDto);
        
        // Then an email with a survey link should be sent to each team member
        var recipient = teamMember.Email;
        var url = "sherpa.com/answer-survey/"+surveyNotificationId;

        var templateRequest = new EmailTemplateRequest(recipient, url);
        Assert.IsType<CreatedResult>(actionResult);
        emailService.Verify(service => service.SendEmailWith(templateRequest));
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}