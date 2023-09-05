using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories.Mongo;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamsAcceptanceTest
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMongoCollection<BsonDocument> _surveyCollection;
    private readonly IMongoCollection<BsonDocument> _teamCollection;
    private readonly IMongoCollection<BsonDocument> _teamMemberCollection;
    private readonly IMongoCollection<BsonDocument> _templateCollection;
    
    public TeamsAcceptanceTest()
    {
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

        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);

        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamMembersCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
    }
    
    [Fact]
    public async Task ShouldBeAbleToCreateTeamAndGetUpdatedListOfTeamsWithNewOne()
    {
        var emptyTeamsList = new List<Team>();
        var inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);
        var teamService = new TeamService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);

        const string teamName = "New team";
        var newTeam = new Team(teamName);

        await teamController.AddTeamAsync(newTeam);

        var actualTeams = await teamController.GetAllTeamsAsync();
        var okObjectResult = Assert.IsType<OkObjectResult>(actualTeams.Result);
        Assert.Equal(emptyTeamsList, okObjectResult.Value);
    }
    
    [Fact]
    public async Task ShouldBeAbleToRetrieveASingleTeamById()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var expectedTeam = new Team(teamId, teamName );
        var teamsList = new List<Team>() { expectedTeam };
        var inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);
        var teamService = new TeamService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);

        var teamByIdAsync = await teamController.GetTeamByIdAsync(teamId);
        
        var resultObject = Assert.IsType<OkObjectResult>(teamByIdAsync.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        Assert.Equal(expectedTeam, resultObject.Value);
    }
}