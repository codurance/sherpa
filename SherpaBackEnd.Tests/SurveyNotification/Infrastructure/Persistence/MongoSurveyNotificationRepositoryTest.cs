using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Test.Helpers;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Tests.Builders;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;

namespace SherpaBackEnd.Tests.SurveyNotification.Infrastructure.Persistence;

[TestSubject(typeof(MongoSurveyNotificationRepository))]
public class MongoSurveyNotificationRepositoryTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();


    private IMongoCollection<BsonDocument> _surveyCollection;
    private IMongoCollection<BsonDocument>? _teamCollection;
    private IMongoCollection<BsonDocument>? _teamMemberCollection;
    private IMongoCollection<BsonDocument>? _templateCollection;
    private IOptions<DatabaseSettings> _databaseSettings;


    [Fact]
    public async Task ShouldCreateAndRetrieveSurveyNotifications()
    {
        await InitializeDbAndCollections();
        var surveyNotificationRepository = new MongoSurveyNotificationRepository(_databaseSettings);

        var firstTeamMember = ATeamMember().WithId(Guid.NewGuid()).Build();
        var secondTeamMember = ATeamMember().WithId(Guid.NewGuid()).Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { firstTeamMember, secondTeamMember })
            .Build();
        var template = TemplateBuilder.ATemplate().Build();
        var deadline = new DateTime().ToUniversalTime();
        var survey = ASurvey().WithId(Guid.NewGuid()).WithDeadline(deadline).WithTeam(team).WithTemplate(template)
            .Build();
        var firstSurveyNotificationId = Guid.NewGuid();
        var firstSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(firstSurveyNotificationId, survey,
                firstTeamMember);
        var secondSurveyNotificationId = Guid.NewGuid();
        var secondSurveyNotification =
            new SherpaBackEnd.SurveyNotification.Domain.SurveyNotification(secondSurveyNotificationId, survey,
                secondTeamMember);

        await _teamCollection.InsertOneAsync(new BsonDocument
            {
                { "_id", team.Id.ToString() },
                { "Name", team.Name },
                { "Members", new BsonArray() { secondTeamMember.Id.ToString(), firstTeamMember.Id.ToString() } },
                { "IsDeleted", team.IsDeleted }
            }
        );

        await _teamMemberCollection.InsertManyAsync(new List<BsonDocument>()
        {
            new BsonDocument
            {
                {
                    "_id", firstTeamMember.Id.ToString()
                },
                {
                    "FullName", firstTeamMember.FullName
                },
                {
                    "Position", firstTeamMember.Position
                },
                {
                    "Email", firstTeamMember.Email
                }
            },
            new BsonDocument
            {
                {
                    "_id", secondTeamMember.Id.ToString()
                },
                {
                    "FullName", secondTeamMember.FullName
                },
                {
                    "Position", secondTeamMember.Position
                },
                {
                    "Email", secondTeamMember.Email
                }
            }
        });

        await _surveyCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", survey.Id.ToString() },
            { "Title", survey.Title },
            { "Status", survey.SurveyStatus },
            { "Deadline", survey.Deadline },
            { "Description", survey.Description },
            { "Team", survey.Team.Id.ToString() },
            { "Template", survey.Template.Name },
            { "Responses", new BsonArray(survey.Responses) },
            {
                "Coach", new BsonDocument
                {
                    { "_id", survey.Coach.Id.ToString() },
                    { "Name", survey.Coach.Name }
                }
            }
        });
        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        var surveyNotifications = new List<SherpaBackEnd.SurveyNotification.Domain.SurveyNotification>()
        {
            firstSurveyNotification,
            secondSurveyNotification
        };
        await surveyNotificationRepository.CreateManySurveyNotification(surveyNotifications);

        var firstCreatedSurveyNotification =
            await surveyNotificationRepository.GetSurveyNotificationById(firstSurveyNotificationId);

        var secondCreatedSurveyNotification =
            await surveyNotificationRepository.GetSurveyNotificationById(secondSurveyNotificationId);

        Assert.Equal(firstSurveyNotificationId, firstCreatedSurveyNotification.Id);
        Assert.Equal(firstTeamMember, firstCreatedSurveyNotification.TeamMember);
        Assert.Equal(survey.Id, firstCreatedSurveyNotification.Survey.Id);

        Assert.Equal(secondSurveyNotificationId, secondCreatedSurveyNotification.Id);
        Assert.Equal(secondTeamMember, secondCreatedSurveyNotification.TeamMember);
        Assert.Equal(survey.Id, secondCreatedSurveyNotification.Survey.Id);
    }


    public async Task InitializeDbAndCollections()
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
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);
        var surveyNotificationCollection =
            mongoDatabase.GetCollection<MSurveyNotification>(_databaseSettings.Value.SurveyNotificationCollectionName);
        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);

        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }
}