using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories.Mongo;

namespace SherpaBackEnd.Tests.Repositories.Mongo;

public class MongoTeamRepositoryTest : IDisposable
{
    private IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017).Build();

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

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }
}