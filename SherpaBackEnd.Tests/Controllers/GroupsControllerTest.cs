using System.Data.Common;
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

    private Mock<IGroupRepository> _mockGroupRepository;
    private GroupsController _groupsController;

    public GroupsControllerTest()
    {
        _mockGroupRepository = new Mock<IGroupRepository>();
        _groupsController = new GroupsController(_mockGroupRepository.Object);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsEmptyList_NotFoundExpected()
    {
        _mockGroupRepository.Setup(repo => repo.getGroups())
            .ReturnsAsync(new List<GroupDTO>());

        var actionResult = await _groupsController.GetGroups();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsList_OkExpected()
    {
        _mockGroupRepository.Setup(repo => repo.getGroups())
            .ReturnsAsync(new List<GroupDTO>{new GroupDTO("Group A"),new GroupDTO("Group B")});

        var actionResult = await _groupsController.GetGroups();
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<GroupDTO>>(objectResult.Value);
        Assert.Equal(2,groups.Count());
    }

    [Fact]
    public async Task GetGroups_RepoThrowsError_ServerErrorExpected()
    {
        var dbException = new RepositoryException("Couldn't connect to the database");
        _mockGroupRepository.Setup(repo => repo.getGroups())
            .ThrowsAsync(dbException);
        var actionResult = await _groupsController.GetGroups();

        var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        
    }
}