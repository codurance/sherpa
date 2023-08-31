
using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories.Team;
using Testcontainers.MongoDb;

namespace SherpaBackEnd.Tests.Repositories.Team;

public class MongoTeamRepositoryTest
{
    [Fact]
    public async Task ShouldReturnTeamByIdIfTeamExists()
    {
        var logger = new Logger<MongoTeamRepository>(new LoggerFactory());
        var mongoDbContainer = new MongoDbContainer(new MongoDbConfiguration("username", "password"), logger);
        await mongoDbContainer.StartAsync();

        var teamId = "7ae4152c-fb06-49c6-976d-957c60371fe7";

        await mongoDbContainer.ExecScriptAsync("use Sherpa");
        await mongoDbContainer.ExecScriptAsync("db.createCollection('Teams')");
        await mongoDbContainer.ExecScriptAsync("db.createCollection('TeamMembers')");
        await mongoDbContainer.ExecScriptAsync($"db.Teams.insertOne({{ _id: '{teamId}', Name: 'Team 1', Members: [], IsDeleted: false }})");

        Environment.SetEnvironmentVariable("CONNECTION_STRING", mongoDbContainer.GetConnectionString());
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers"
        };
            
        var mongoTeamRepository = new MongoTeamRepository(Options.Create(databaseSettings));
        
        var team = await mongoTeamRepository.GetTeamByIdAsync(Guid.Parse(teamId));
        
        Assert.Equal(teamId, team?.Id.ToString());
    }
}