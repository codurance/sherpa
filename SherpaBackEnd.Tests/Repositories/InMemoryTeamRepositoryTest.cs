using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemoryTeamRepositoryTest
{
    [Fact]
    public async Task ShouldBeAbleToAddNewTeam()
    {
        var initialList = new List<Team>();
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        const string teamName = "Test Name";
        var newTeam = new Team(teamName);
        
        await inMemoryTeamRepository.AddTeamAsync(newTeam);
        
        Assert.Contains(newTeam, initialList);
    }
    
    [Fact]
    public async Task ShouldBeAbleToGetAllTeams()
    {
        const string teamName1 = "Team 1";
        var existingTeam1 = new Team(teamName1);
        const string teamName2 = "Team 2";
        var existingTeam2 = new Team(teamName2);
        
        var initialList = new List<Team>(){existingTeam1, existingTeam2};
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var retrievedTeams = await inMemoryTeamRepository.GetAllTeamsAsync();
        
        Assert.Equal(new List<Team>(){existingTeam1, existingTeam2}, retrievedTeams);
    }
    
    [Fact]
    public async Task ShouldBeAbleToGetTeamById()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Team 1";
        var expectedTeam = new Team(teamId, teamName);
        
        var initialList = new List<Team>(){expectedTeam};
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var actualTeam = await inMemoryTeamRepository.GetTeamByIdAsync(teamId);
        
        Assert.Equal(expectedTeam, actualTeam);
    }
}