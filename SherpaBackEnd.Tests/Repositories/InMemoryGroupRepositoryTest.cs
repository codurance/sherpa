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
    public void ShouldBeAbleToGetANewGroup()
    {
        const string teamName = "Test Name";
        var existingTeam = new Group(teamName);
        
        var initialList = new List<Group>();
        initialList.Add(existingTeam);
        var inMemoryGroupRepository = new InMemoryGroupRepository(initialList);

        var retrievedTeam = inMemoryGroupRepository.GetAllTeams();
        
        Assert.Equal(retrievedTeam, retrievedTeam);
    }
}