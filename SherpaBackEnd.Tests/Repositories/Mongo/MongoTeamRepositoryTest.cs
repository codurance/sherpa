using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;

namespace SherpaBackEnd.Tests.Repositories.Mongo;

public class MongoTeamRepositoryTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    [Fact]
    public async Task ShouldReturnTeamByIdIfTeamExists()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        
        // Seed the container db with a team
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamsCollectionName);

        await collection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", "Test Team" },
                { "Members", new BsonArray() },
                { "IsDeleted", "false" }
            }
        );

        // act
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        var team = await mongoTeamRepository.GetTeamByIdAsync(teamId);

        Assert.Equal(teamId, team?.Id);
    }
    
    [Fact]
    public async Task ShouldReturnNullIfTeamDoesNotExists()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        var team = await mongoTeamRepository.GetTeamByIdAsync(teamId);

        Assert.Null(team);
    }
    
    [Fact]
    public async Task ShouldAddANewTeam()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        await mongoTeamRepository.AddTeamAsync(
            new Team.Domain.Team(teamId, "Demo Team"));

        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamsCollectionName);
        
        var filter = Builders<BsonDocument>.Filter.Eq("_id", teamId.ToString());
        var mTeam = await collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        
        Assert.NotNull(mTeam);
    }
    
    [Fact]
    public async Task ShouldReturnAllTeams()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        
        // Seed the container db with a team
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamsCollectionName);

        await collection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", "Test Team" },
                { "Members", new BsonArray() },
                { "IsDeleted", "false" }
            }
        );

        // act
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        var teams = await mongoTeamRepository.GetAllTeamsAsync();

        Assert.Single(teams);

        Assert.Equal(teamId, teams.First().Id);
    }
    
    [Fact]
    public async Task ShouldAddTeamMemberToTeam()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        
        // Seed the container db with a team
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamsCollectionName);

        await collection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", "Test Team" },
                { "Members", new BsonArray() },
                { "IsDeleted", "false" }
            }
        );

        // act
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        var teamMemberId = Guid.NewGuid();
        
        await mongoTeamRepository.AddTeamMemberToTeamAsync(
            teamId,
            new TeamMember(teamMemberId, "Test User", "Position", "email@email.com"));
        
        var team = await mongoTeamRepository.GetTeamByIdAsync(teamId);
        
        Assert.Single(team?.Members);
        Assert.Equal(teamMemberId, team?.Members.First().Id);
    }
    
    [Fact]
    public async Task ShouldGetAllTeamMembersOfSpecificTeam()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };

        var teamId = Guid.NewGuid();
        var teamMemberId = Guid.NewGuid();
        
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamsCollectionName);
        
        var teamMemberCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TeamMembersCollectionName);

        await teamCollection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", "Test Team" },
                { "Members", new BsonArray(){teamMemberId.ToString()} },
                { "IsDeleted", "false" }
            }
        );
        
        await teamMemberCollection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamMemberId.ToString() },
                { "FullName", "Test User" },
                { "Position", "Position" },
                { "Email", "email@email.com" },
            }
        );
        
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));

        var allTeamMembers = await mongoTeamRepository.GetAllTeamMembersAsync(teamId);
        
        Assert.Single(allTeamMembers);
        Assert.Equal(teamMemberId, allTeamMembers.First().Id);
    }

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }
}