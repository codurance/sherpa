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
}