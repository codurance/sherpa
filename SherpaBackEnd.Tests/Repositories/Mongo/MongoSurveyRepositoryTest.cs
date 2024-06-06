using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Tests.Builders;

namespace SherpaBackEnd.Tests.Repositories.Mongo;

public class MongoSurveyRepositoryTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private IMongoCollection<BsonDocument> _surveyCollection;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument>? _teamCollection;
    private IMongoCollection<BsonDocument>? _teamMemberCollection;
    private IMongoCollection<BsonDocument>? _templateCollection;
    private IMongoDatabase? _mongoDatabase;

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }

    [Fact]
    public async Task ShouldBeAbleToCreateSurvey()
    {
        await InitialiseDatabaseClientAndCollections();

        var surveyCollection = _mongoDatabase.GetCollection<MSurvey>(
            _databaseSettings.Value.SurveyCollectionName);

        var surveyRepository = new MongoSurveyRepository(_databaseSettings);

        var survey = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Lucia"),
            SurveyStatus.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", new List<SurveyResponse>(),
            new Team.Domain.Team(Guid.NewGuid(), "team name"),
            new Template.Domain.Template("Template name", Array.Empty<IQuestion>(), 1));

        await surveyRepository.CreateSurvey(survey);

        var insertedSurvey = await surveyCollection.Find(new BsonDocument()).FirstOrDefaultAsync();


        Assert.Equal(survey.Id, insertedSurvey?.Id);
        Assert.Equal(survey.Team.Id, insertedSurvey?.Team);
        Assert.Equal(survey.Template.Name, insertedSurvey?.Template);
    }

    private async Task InitialiseDatabaseClientAndCollections()
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

        _mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);

        _teamCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);

        _surveyCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }

    [Fact]
    public async Task ShouldReturnAllSurveysFromTeam()
    {
        await InitialiseDatabaseClientAndCollections();

        var teamMemberId = Guid.NewGuid();
        var teamMember = new TeamMember(teamMemberId, "Some name", "Some position", "some@email.com");
        var teamId = Guid.NewGuid();
        var team = new Team.Domain.Team(teamId, "Some team name", new List<TeamMember> { teamMember });
        var template = new Template.Domain.Template("Hackman Model", new List<IQuestion>(), 10);
        var survey = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Demo coach"),
            SurveyStatus.Draft, DateTime.Now,
            "Survey title", "Survey Description", new List<SurveyResponse>(), team, template);

        var survey2 = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Demo coach"),
            SurveyStatus.Draft, DateTime.Now,
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


        var surveyRepository = new MongoSurveyRepository(_databaseSettings);

        var foundSurveys = await surveyRepository.GetAllSurveysFromTeam(teamId);

        Assert.Equal(2, foundSurveys.Count());
    }

    [Fact]
    public async Task ShouldReturnEmptyListOfSurveysIfNotFoundAnyMatchingByTeamId()
    {
        await InitialiseDatabaseClientAndCollections();

        var teamId = Guid.NewGuid();
        var team = new Team.Domain.Team(teamId, "Some team name", new List<TeamMember> { });

        _teamCollection.InsertOne(new BsonDocument
        {
            { "_id", teamId.ToString() },
            { "Name", team.Name },
            { "Members", new BsonArray() },
            { "IsDeleted", team.IsDeleted }
        });

        var surveyRepository = new MongoSurveyRepository(_databaseSettings);

        var foundSurveys = await surveyRepository.GetAllSurveysFromTeam(teamId);

        Assert.False(foundSurveys.Any());
    }


    [Fact]
    public async Task ShouldUpdateSurvey()
    {
        await InitialiseDatabaseClientAndCollections();
        var template = TemplateBuilder.ATemplate().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();
        await _teamCollection.InsertOneAsync(new BsonDocument
            {
                { "_id", team.Id.ToString() },
                { "Name", team.Name },
                { "Members", new BsonArray() { teamMember.Id.ToString() } },
                { "IsDeleted", team.IsDeleted }
            }
        );

        await _teamMemberCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", teamMember.Id.ToString() },
            { "FullName", teamMember.FullName },
            { "Position", teamMember.Position },
            { "Email", teamMember.Email }
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
        var mongoSurveyRepository = new MongoSurveyRepository(_databaseSettings);

        SurveyResponse response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        survey.AnswerSurvey(response);
        await mongoSurveyRepository.Update(survey);

        var updatedSurvey = await mongoSurveyRepository.GetSurveyById(survey.Id);
        
        Assert.Contains(response, updatedSurvey.Responses);
    }
}