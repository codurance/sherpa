using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories.Mongo;

namespace SherpaBackEnd.Tests.Repositories.Mongo;

public class MongoSurveyRepositoryTest: IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }

    [Fact]
    public async Task ShouldBeAbleToCreateSurvey()
    {
        await _mongoDbContainer.StartAsync();
        
        var databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        var surveyCollection = mongoDatabase.GetCollection<MSurvey>(
            databaseSettings.Value.SurveyCollectionName);

        var surveyRepository = new MongoSurveyRepository(databaseSettings);

        var survey = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", new List<Response>(),
            new Dtos.Team(Guid.NewGuid(), "team name"), new Template("Template name", Array.Empty<IQuestion>(), 1));

        await surveyRepository.CreateSurvey(survey);

        var insertedSurvey = await surveyCollection.Find(new BsonDocument()).FirstOrDefaultAsync();

        Assert.Equal(survey.Id, insertedSurvey?.Id);
        Assert.Equal(survey.Team.Id, insertedSurvey?.Team);
        Assert.Equal(survey.Template.Name, insertedSurvey?.Template);
    }

    [Fact]
    public async Task ShouldReturnAllSurveysFromTeam()
    {
        await _mongoDbContainer.StartAsync();
        
        var databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        var teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TeamsCollectionName);

        var teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TeamMembersCollectionName);

        var templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TemplateCollectionName);
        
        var surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.SurveyCollectionName);


        var teamMemberId = Guid.NewGuid();
        var teamMember = new TeamMember(teamMemberId, "Some name", "Some position", "some@email.com");
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, "Some team name", new List<TeamMember> { teamMember });
        var template = new Template("Hackman Model", new List<IQuestion>(), 10);
        var survey = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Demo coach"), Status.Draft, DateTime.Now,
            "Survey title", "Survey Description", new List<Response>(), team, template);

        var survey2 = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Demo coach"), Status.Draft, DateTime.Now,
            "Survey title", "Survey Description", new List<Response>(), team, template);

        teamMemberCollection.InsertOne(new BsonDocument
        {
            { "_id", teamMemberId.ToString() },
            { "FullName", teamMember.FullName },
            { "Position", teamMember.Position },
            { "Email", teamMember.Email },
        });

        teamCollection.InsertOne(new BsonDocument
        {
            { "_id", teamId.ToString() },
            { "Name", team.Name },
            { "Members", new BsonArray() { teamMemberId.ToString() } },
            { "IsDeleted", team.IsDeleted }
        });

        templateCollection.InsertOne(new BsonDocument
        {
            { "name", template.Name },
            { "questions", new BsonArray() },
            { "minutesToComplete", template.MinutesToComplete }
        });

        surveyCollection.InsertMany(new List<BsonDocument>()
        {
            new BsonDocument
            {
                { "_id", survey.Id.ToString() },
                { "Coach", survey.Coach.Id.ToString() },
                { "Status", survey.Status.ToString() },
                { "Title", survey.Title },
                { "Description", survey.Description },
                { "Responses", new BsonArray() },
                { "Team", survey.Team.Id.ToString() },
                { "Template", survey.Template.Name }
            },
            new BsonDocument
            {
                { "_id", survey2.Id.ToString() },
                { "Coach", survey2.Coach.Id.ToString() },
                { "Status", survey2.Status.ToString() },
                { "Title", survey2.Title },
                { "Description", survey2.Description },
                { "Responses", new BsonArray() },
                { "Team", survey2.Team.Id.ToString() },
                { "Template", survey2.Template.Name }
            }
        });


        var surveyRepository = new MongoSurveyRepository(databaseSettings);

        var foundSurveys = await surveyRepository.GetAllSurveysFromTeam(teamId);

        Assert.Equal(2, foundSurveys.Count());
    }

    [Fact]
    public async Task ShouldReturnEmptyListOfSurveysIfNotFoundAnyMatchingByTeamId()
    {
        await _mongoDbContainer.StartAsync();
        
        var databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });
        
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        var teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TeamsCollectionName);

        var teamId = Guid.NewGuid();
        var team = new Team(teamId, "Some team name", new List<TeamMember> { });

        teamCollection.InsertOne(new BsonDocument
        {
            { "_id", teamId.ToString() },
            { "Name", team.Name },
            { "Members", new BsonArray()},
            { "IsDeleted", team.IsDeleted }
        });

        var surveyRepository = new MongoSurveyRepository(databaseSettings);

        var foundSurveys = await surveyRepository.GetAllSurveysFromTeam(teamId);

        Assert.False(foundSurveys.Any());
    }
}