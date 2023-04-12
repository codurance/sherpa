using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupsControllerAcceptanceTest
{
    [Fact]
    public async Task GetGroupsReturnCorrectNumberOfGroupsAndMembersInside()
    {
        var inMemoryGroupRepository = new InMemoryGroupRepository();
        var groupsController = new GroupsController(inMemoryGroupRepository);

        var groupsActionResult = await groupsController.GetGroupsAsync();
        var groupsObjectResult = Assert.IsType<OkObjectResult>(groupsActionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<Group>>(groupsObjectResult.Value);
        Assert.Equal(2, groups.Count());
        
        var firstGroupId = groups.First().Id;
        var firstGroupName = groups.First().Name;

        var singleGroupActionResult = await groupsController.GetGroupAsync(firstGroupId);
        var singleGroupObjectResult = Assert.IsType<OkObjectResult>(singleGroupActionResult.Result);
        var singleGroup = Assert.IsAssignableFrom<Group>(singleGroupObjectResult.Value);
       
        Assert.Equal(firstGroupId, singleGroup.Id);
        Assert.Equal(firstGroupName, singleGroup.Name);
        Assert.Equal(3, singleGroup.Members.Count());
    }
    
    
    
}