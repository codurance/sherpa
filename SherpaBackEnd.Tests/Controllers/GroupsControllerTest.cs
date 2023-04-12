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

        var actionResult = await _groupsController.GetGroups();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsList_OkExpected()
    {
        _mockGroupRepository.Setup(repo => repo.GetGroups())
            .ReturnsAsync(new List<Group>{new("Group A"),new("Group B")});

        var actionResult = await _groupsController.GetGroups();
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
        var actionResult = await _groupsController.GetGroups();

        var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        
    }

    [Fact]
    public async Task GetGroupById_RepoReturnsEmptyGroup_OkExpected()
    {
        var expectedGroup = new Group("Group A");
        var guid = expectedGroup.Id;
        
        _mockGroupRepository.Setup(m => m.GetGroup(guid)).ReturnsAsync(expectedGroup);
        var actionResult = await _groupsController.GetGroup(guid);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualGroup = Assert.IsAssignableFrom<Group>(okObjectResult.Value);
        Assert.Empty(actualGroup.Members);
    }
}