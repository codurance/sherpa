using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamsAcceptanceTest
{
    [Fact]
    public async void ShouldBeAbleToCreateTeamAndGetUpdatedListOfTeamsWithNewOne()
    {
        var emptyGroupList = new List<Group>();
        var inMemoryGroupRepository = new InMemoryGroupRepository(emptyGroupList);
        var groupsService = new GroupsService(inMemoryGroupRepository);
        var groupsController = new GroupsController(groupsService);

        const string groupName = "New group";
        var newGroup = new Group(groupName);

        await groupsController.AddTeamAsync(newGroup);

        var actualGroups = await groupsController.GetGroupsAsync();
        Assert.Equal(emptyGroupList, actualGroups);
    }
}