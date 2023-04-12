using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupsControllerTest
{

    private Mock<IGroupRepository> mockGroupRepository;
    private GroupsController _groupsController;

    public GroupsControllerTest()
    {
        this.mockGroupRepository = new Mock<IGroupRepository>();
        this._groupsController = new GroupsController(mockGroupRepository.Object);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsEmptyList_NotFoundExpected()
    {
        mockGroupRepository.Setup(repo => repo.getGroups())
            .ReturnsAsync(new List<GroupDTO>());

        var actionResult = await _groupsController.getGroups();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetGroups_RepoReturnsList_OkExpected()
    {
        mockGroupRepository.Setup(repo => repo.getGroups())
            .ReturnsAsync(new List<GroupDTO>{new GroupDTO("Group A"),new GroupDTO("Group B")});

        var actionResult = await _groupsController.getGroups();
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var groups = Assert.IsAssignableFrom<IEnumerable<GroupDTO>>(objectResult.Value);
        Assert.Equal(2,groups.Count());
    }
}