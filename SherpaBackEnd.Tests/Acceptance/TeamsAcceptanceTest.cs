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

    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _surveyCollection;
    private IMongoCollection<BsonDocument> _teamCollection;
    private IMongoCollection<BsonDocument> _teamMemberCollection;
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
    public async Task ShouldBeAbleToCreateTeamAndGetUpdatedListOfTeamsWithNewOne()
    {
        await InitializeDbClientAndCollections();
        
        var inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);
        var teamService = new TeamService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);

        const string teamName = "New team";
        var newTeam = new Team(teamName);

        await teamController.AddTeamAsync(newTeam);

        var actualTeams = await teamController.GetAllTeamsAsync();
        var okObjectResult = Assert.IsType<OkObjectResult>(actualTeams.Result);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        var teams = Assert.IsAssignableFrom<IEnumerable<Team>>(okObjectResult.Value);
        Assert.Contains(teams, team => team.Id == newTeam.Id);
    }
    
    [Fact]
    public async Task ShouldBeAbleToRetrieveASingleTeamById()
    {
        await InitializeDbClientAndCollections();
        
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        
        
        var memberId = Guid.NewGuid();
        var newTeamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        await _teamMemberCollection.InsertOneAsync(
            new BsonDocument() {
                { "_id", memberId.ToString() },
                { "FullName", newTeamMember.FullName },
                { "Position", newTeamMember.Position },
                { "Email", newTeamMember.Email },
            });
        
        var expectedTeam = new Team(teamId, teamName, new List<TeamMember>(){newTeamMember});
        
        await _teamCollection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", teamName },
                { "Members", new BsonArray() {memberId.ToString()} },
                { "IsDeleted", false }
            });

        var inMemoryTeamRepository = new MongoTeamRepository(_databaseSettings);
        var teamService = new TeamService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);

        var teamByIdAsync = await teamController.GetTeamByIdAsync(teamId);
        
        var resultObject = Assert.IsType<OkObjectResult>(teamByIdAsync.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        CustomAssertions.StringifyEquals(expectedTeam, resultObject.Value);
    }
}