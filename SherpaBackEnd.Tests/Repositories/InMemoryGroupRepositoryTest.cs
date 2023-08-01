using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemoryGroupRepositoryTest
{
    [Fact]

    public void ShouldBeAbleToAddNewGroup()
    {
        var initialList = new List<Group>();
        var inMemoryGroupRepository = new InMemoryGroupRepository(initialList);

        const string teamName = "Test Name";
        var newTeam = new Group(teamName);
        
        inMemoryGroupRepository.AddTeam(newTeam);
        
        Assert.Contains(newTeam, initialList);
    }
    
    [Fact]
    public void ShouldBeAbleToGetAllGroups()
    {
        const string teamName1 = "Team 1";
        var existingTeam1 = new Group(teamName1);
        const string teamName2 = "Team 2";
        var existingTeam2 = new Group(teamName2);
        
        var initialList = new List<Group>(){existingTeam1, existingTeam2};
        var inMemoryGroupRepository = new InMemoryGroupRepository(initialList);

        var retrievedTeams = inMemoryGroupRepository.GetAllTeams();
        
        Assert.Equal(new List<Group>(){existingTeam1, existingTeam2}, retrievedTeams);
    }
}