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

        var actionResult = await _groupsController.AddGroupAsync(expectedGroup);
        Assert.IsType<OkResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewGroupWithoutName_BadRequestResultExpected()
    {
        var expectedGroup = new Group("");

        var actionResult = await _groupsController.AddGroupAsync(expectedGroup);
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
        
        var actionResult = await _groupsController.UpdateGroupAsync(Guid.NewGuid(), new Group("name"));
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
        
        await _groupsController.UpdateGroupAsync(group.Id,group);
     
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
        
        await _groupsController.UpdateGroupAsync(group.Id,group);
     
        _mockGroupService.Verify(repo => repo.UpdateGroup(It.Is<Group>(updatedGroup => updatedGroup.Id.Equals(group.Id))));
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromService()
    {
        const string teamName = "Team name";
        var newTeam = new Group(teamName);

        await _groupsController.AddTeamAsync(newTeam);
        
        _mockGroupService.Verify(_ => _.AddTeamAsync(newTeam), Times.Once());
    }

    [Fact]
    public async Task ShouldRetrieveOkWhenNoProblemsFoundWhileAdding()
    {
        var newTeam = new Group("New Team");
        Assert.IsType<CreatedResult>(await _groupsController.AddTeamAsync(newTeam));
    }
    
    [Fact]
    public async Task ShouldRetrieveErrorIfTeamCannotBeAdded()
    {
        var newTeam = new Group("New Team");
        var notSuccessfulAdding = new RepositoryException("Cannot perform add team function.");
        _mockGroupService.Setup(_ => _.AddTeamAsync(newTeam))
            .ThrowsAsync(notSuccessfulAdding);
            
        var addingResult = await _groupsController.AddTeamAsync(newTeam);

        var resultObject = Assert.IsType<ObjectResult>(addingResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }

    [Fact]
    public async Task ShouldCallGetAllTeamsMethodFromService()
    {
        await _groupsController.GetAllTeamsAsync();
        
        _mockGroupService.Verify(_ => _.GetAllTeamsAsync(), Times.Once());
    }
    
    [Fact]
    public async Task ShouldReturnOkWhenEmptyTeamListRetrievedWhileGettingAllTeams()
    {
        var emptyTeamsList = new List<Group>();
        _mockGroupService.Setup(service => service.GetAllTeamsAsync())
            .ReturnsAsync(emptyTeamsList);
        var getAllTeamsAction = await _groupsController.GetAllTeamsAsync();
        Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        
    }
    
    [Fact]
    public async Task ShouldReturnOkWhenTeamListRetrievedWhileGettingAllTeams()
    {
        var allTeamsList = new List<Group>()
        {
            new Group("Team one"),
            new Group("Team two")
        };
        _mockGroupService.Setup(service => service.GetAllTeamsAsync())
            .ReturnsAsync(allTeamsList);
        var getAllTeamsAction = await _groupsController.GetAllTeamsAsync();
        var okObjectResult = Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        Assert.Equal(allTeamsList, okObjectResult.Value);
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfAllTeamCannotBeRetrieved()
    {
        var notSuccessfulGettingAllTeams = new RepositoryException("Cannot perform get all teams function.");
        _mockGroupService.Setup(_ => _.GetAllTeamsAsync())
            .ThrowsAsync(notSuccessfulGettingAllTeams);

        var allTeamsAsync = await _groupsController.GetAllTeamsAsync();

        var resultObject = Assert.IsType<ObjectResult>(allTeamsAsync.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }
}