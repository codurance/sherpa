using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupsControllerAcceptanceTest
{
    [Fact]
    public async Task GetGroupsReturnCorrectNumberOfGroupsAndMembersInside()
    {
        var inMemoryGroupRepository = new InMemoryGroupRepository();
        var groupsService = new GroupsService(inMemoryGroupRepository);
        var groupsController = new GroupsController(groupsService);

        var groupsActionResult = await groupsController.GetGroupsAsync();
        var groupsObjectResult = Assert.IsType<OkObjectResult>(groupsActionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<Group>>(groupsObjectResult.Value);
        Assert.Equal(2, groups.Count());

        var actualFirstGroup = groups.First();
        var firstGroupId = actualFirstGroup.Id;
        var firstGroupName = actualFirstGroup.Name;

        var singleGroupActionResult = await groupsController.GetGroupAsync(firstGroupId);
        var singleGroupObjectResult = Assert.IsType<OkObjectResult>(singleGroupActionResult.Result);
        var actualSingleByIdGroup = Assert.IsAssignableFrom<Group>(singleGroupObjectResult.Value);
       
        Assert.Equal(firstGroupId, actualSingleByIdGroup.Id);
        Assert.Equal(firstGroupName, actualSingleByIdGroup.Name);
    }


    [Fact]
    public async Task GetGroupsListIsUpdatedAfterDelete()
    {
        var inMemoryGroupRepository = new InMemoryGroupRepository();
        var groupsService = new GroupsService(inMemoryGroupRepository);
        var groupsController = new GroupsController(groupsService);

        var groupsActionResult = await groupsController.GetGroupsAsync();
        var groupsObjectResult = Assert.IsType<OkObjectResult>(groupsActionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<Group>>(groupsObjectResult.Value).ToList();
        Assert.Equal(2, groups.Count());

        var groupToDelete = groups.First();
        await groupsController.DeleteGroupAsync(groupToDelete.Id);

        var groupsAfterDeleteActionResult = await groupsController.GetGroupsAsync();
        var groupsAfterDeleteObjectResult = Assert.IsType<OkObjectResult>(groupsAfterDeleteActionResult.Result);
        var groupsAfterDelete = Assert.IsAssignableFrom<IEnumerable<Group>>(groupsAfterDeleteObjectResult.Value).ToList();
        Assert.Single(groupsAfterDelete);

        Assert.DoesNotContain(groupsAfterDelete, g => g.Id == groupToDelete.Id);
    }
}