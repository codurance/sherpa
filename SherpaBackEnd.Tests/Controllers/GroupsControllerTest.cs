using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupsControllerTest
{

    private readonly Mock<IGroupsService> _mockGroupService;
    private readonly GroupsController _groupsController;

    public GroupsControllerTest()
    {
        _mockGroupService = new Mock<IGroupsService>();
        _groupsController = new GroupsController(_mockGroupService.Object);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsEmptyList_NotFoundExpected()
    {
        _mockGroupService.Setup(service => service.GetGroups())
            .ReturnsAsync(new List<Group>());

        var actionResult = await _groupsController.GetGroupsAsync();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsList_OkExpected()
    {
        _mockGroupService.Setup(repo => repo.GetGroups())
            .ReturnsAsync(new List<Group>{new("Group A"),new("Group B")});

        var actionResult = await _groupsController.GetGroupsAsync();
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<Group>>(objectResult.Value);
        Assert.Equal(2,groups.Count());
    }

    [Fact]
    public async Task GetGroups_RepoThrowsError_ServerErrorExpected()
    {
        var dbException = new RepositoryException("Couldn't connect to the database");
        _mockGroupService.Setup(repo => repo.GetGroups())
            .ThrowsAsync(dbException);
        var actionResult = await _groupsController.GetGroupsAsync();

        var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        
    }

    [Fact]
    public async Task GetGroupById_RepoReturnsEmptyGroup_OkExpected()
    {
        var expectedGroup = new Group("Group");
        var guid = expectedGroup.Id;
        
        _mockGroupService.Setup(m => m.GetGroup(guid)).ReturnsAsync(expectedGroup);
        var actionResult = await _groupsController.GetGroupAsync(guid);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualGroup = Assert.IsAssignableFrom<Group>(okObjectResult.Value);
        Assert.Empty(actualGroup.Members);
    }


    [Fact]
    public async Task GetGroupById_RepoDoesntReturnGroup_NotFoundExpected()
    {
        _mockGroupService.Setup(m => m.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.GetGroupAsync(new Guid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewGroup_OkResultExpected()
    {
        var expectedGroup = new Group("New group");

        _mockGroupService.Setup(m => m.AddGroup(It.IsAny<Group>()));

        var actionResult = await _groupsController.AddGroup(expectedGroup);
        Assert.IsType<OkResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewGroupWithoutName_BadRequestResultExpected()
    {
        var expectedGroup = new Group("");

        var actionResult = await _groupsController.AddGroup(expectedGroup);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task DeleteGroup_GroupDoesNotExist_ExpectedNotFound()
    {
        _mockGroupService.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.DeleteGroupAsync(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteGroup_GroupExists_ExpectedOkResult()
    {
        var group = new Group("Deleting Group");
        _mockGroupService.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);
        
        var actionResult = await _groupsController.DeleteGroupAsync(group.Id);
        Assert.IsType<OkResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteGroup_GroupExists_VerifyInteractionWithRepository()
    {
        var group = new Group("Deleting Group");
        _mockGroupService.Setup(repo => repo.GetGroup(group.Id))
            .ReturnsAsync(group);
        
        await _groupsController.DeleteGroupAsync(group.Id);
        _mockGroupService.Verify(repo => repo.GetGroup(group.Id));
        Assert.True(group.IsDeleted);
        _mockGroupService.Verify(repo => repo.UpdateGroup(group));
    }
    
    [Fact]
    public async Task UpdateGroup_GroupDoesNotExist_ExpectedNotFound()
    {
        _mockGroupService.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.UpdateGroup(Guid.NewGuid(), new Group("name"));
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task UpdateGroup_GroupExists_AssertEntireGroupIsPassed()
    {
        var group = new Group("Group A");
        group.Members = new List<GroupMember>
        {
            new ("Name A", "lastName A", "position A", "e1@e.com"),
            new ("Name B", "lastName B", "position B", "e2@e.com")
        };
        
        _mockGroupService.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);

        var members = group.Members.ToList();
        members.Add(new GroupMember("Name C", "Lastname C", "position C", "e3@e.com"));
        group.Members = members;
        
        await _groupsController.UpdateGroup(group.Id,group);
     
        _mockGroupService.Verify(repo => repo.UpdateGroup(It.Is<Group>(updatedGroup => updatedGroup.Members.ToList().Count.Equals(3))));
    }
    
    [Fact]
    public async Task UpdateGroup_GroupExists_AssertRightIdIsPassed()
    {
        var group = new Group("Group A");
        group.Members = new List<GroupMember>
        {
            new ("Name A", "lastName A", "position A", "e1@e.com"),
            new ("Name B", "lastName B", "position B", "e2@e.com")
        };
        
        _mockGroupService.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);

        group.Name = "updated name";
        
        await _groupsController.UpdateGroup(group.Id,group);
     
        _mockGroupService.Verify(repo => repo.UpdateGroup(It.Is<Group>(updatedGroup => updatedGroup.Id.Equals(group.Id))));
    }
}