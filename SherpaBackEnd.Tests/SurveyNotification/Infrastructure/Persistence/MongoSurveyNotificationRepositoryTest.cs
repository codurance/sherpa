using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
namespace SherpaBackEnd.Tests.SurveyNotification.Infrastructure.Persistence;

[TestSubject(typeof(MongoSurveyNotificationRepository))]
public class MongoSurveyNotificationRepositoryTest : IDisposable
{
    
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();
    
    [Fact]
    public async Task ShouldCreateSurveyNotifications()
    {
        await _mongoDbContainer.StartAsync();
        var databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            SurveyNotificationCollectionName = "SurveyNotifications",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });

        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        var surveyNotificationCollection =
            mongoDatabase.GetCollection<MSurveyNotification>(databaseSettings.Value.SurveyNotificationCollectionName);

        var surveyNotificationRepository = new MongoSurveyNotificationRepository(databaseSettings);

        var firstTeamMember = ATeamMember().WithId(Guid.NewGuid()).Build();
        var firstSurvey = ASurvey().WithId(Guid.NewGuid()).Build();
        var firstSurveyNotificationId = Guid.NewGuid();
        var firstSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(firstSurveyNotificationId, firstSurvey, firstTeamMember );
        var secondTeamMember = ATeamMember().WithId(Guid.NewGuid()).Build();
        var secondSurvey = ASurvey().WithId(Guid.NewGuid()).Build();
        var secondSurveyNotificationId = Guid.NewGuid();
        var secondSurveyNotification = new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(secondSurveyNotificationId, secondSurvey, secondTeamMember );
        
        var surveyNotifications = new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
        {
            firstSurveyNotification,
            secondSurveyNotification
        };
        await surveyNotificationRepository.CreateManySurveyNotification(surveyNotifications);
        
        var firstCreatedSurveyNotification = await surveyNotificationCollection
            .Find(notification => notification.Id == firstSurveyNotificationId).FirstOrDefaultAsync();

        var secondCreatedSurveyNotification = await surveyNotificationCollection
            .Find(notification => notification.Id == secondSurveyNotificationId).FirstOrDefaultAsync();
        
        Assert.Equal(firstSurveyNotificationId, firstCreatedSurveyNotification.Id);
        Assert.Equal(firstTeamMember.Id, firstCreatedSurveyNotification.TeamMember);
        Assert.Equal(firstSurvey.Id, firstCreatedSurveyNotification.Survey);
        
        Assert.Equal(secondSurveyNotificationId, secondCreatedSurveyNotification.Id);
        Assert.Equal(secondTeamMember.Id, secondCreatedSurveyNotification.TeamMember);
        Assert.Equal(secondSurvey.Id, secondCreatedSurveyNotification.Survey);
    }
    

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }
}