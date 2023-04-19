using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupsControllerTest
{

    private readonly Mock<IGroupRepository> _mockGroupRepository;
    private readonly GroupsController _groupsController;

    public GroupsControllerTest()
    {
        _mockGroupRepository = new Mock<IGroupRepository>();
        _groupsController = new GroupsController(_mockGroupRepository.Object);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsEmptyList_NotFoundExpected()
    {
        _mockGroupRepository.Setup(repo => repo.GetGroups())
            .ReturnsAsync(new List<Group>());

        var actionResult = await _groupsController.GetGroupsAsync();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsList_OkExpected()
    {
        _mockGroupRepository.Setup(repo => repo.GetGroups())
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
        _mockGroupRepository.Setup(repo => repo.GetGroups())
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
        
        _mockGroupRepository.Setup(m => m.GetGroup(guid)).ReturnsAsync(expectedGroup);
        var actionResult = await _groupsController.GetGroupAsync(guid);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualGroup = Assert.IsAssignableFrom<Group>(okObjectResult.Value);
        Assert.Empty(actualGroup.Members);
    }


    [Fact]
    public async Task GetGroupById_RepoDoesntReturnGroup_NotFoundExpected()
    {
        _mockGroupRepository.Setup(m => m.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.GetGroupAsync(new Guid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewGroup_OkResultExpected()
    {
        var expectedGroup = new Group("New group");

        _mockGroupRepository.Setup(m => m.AddGroup(It.IsAny<Group>()));

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
        _mockGroupRepository.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.DeleteGroup(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteGroup_GroupExists_ExpectedOkResult()
    {
        var group = new Group("Deleting Group");
        _mockGroupRepository.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);
        
        var actionResult = await _groupsController.DeleteGroup(group.Id);
        Assert.IsType<OkResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task UpdateGroup_GroupDoesNotExist_ExpectedNotFound()
    {
        _mockGroupRepository.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync((Group)null);
        
        var actionResult = await _groupsController.UpdateGroup(Guid.NewGuid(), new Group("name"));
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task UpdateGroup_GroupExists_AssertENtireGroupIsPassed()
    {
        var group = new Group("Group A");
        group.Members = new List<GroupMember>
        {
            new GroupMember("Name A", "lastName A", "position A"),
            new GroupMember("Name B", "lastName B", "position B")
        };
        
        _mockGroupRepository.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);

        List<GroupMember> members = group.Members.ToList();
        members.Add(new GroupMember("Name C", "Lastname C", "position C"));
        group.Members = members;
        
        await _groupsController.UpdateGroup(group.Id,group);
     
        _mockGroupRepository.Verify(repo => repo.UpdateGroup(It.Is<Group>(updatedGroup => updatedGroup.Members.ToList().Count.Equals(3))));
    }
    
    
    [Fact]
    public async Task UpdateGroup_GroupExists_AssertRightIdIsPassed()
    {
        var group = new Group("Group A");
        group.Members = new List<GroupMember>
        {
            new GroupMember("Name A", "lastName A", "position A"),
            new GroupMember("Name B", "lastName B", "position B")
        };
        
        _mockGroupRepository.Setup(repo => repo.GetGroup(It.IsAny<Guid>()))
            .ReturnsAsync(group);

        group.Name = "updated name";
        
        await _groupsController.UpdateGroup(group.Id,group);
     
        _mockGroupRepository.Verify(repo => repo.UpdateGroup(It.Is<Group>(updatedGroup => updatedGroup.Id.Equals(group.Id))));
    }
}