using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class GroupServiceTest
{
    private readonly Mock<IGroupRepository> _mockGroupRepository;
    private readonly GroupsService _groupService;

    public GroupServiceTest()
    {
        _mockGroupRepository = new Mock<IGroupRepository>();
        _groupService = new GroupsService(_mockGroupRepository.Object);
    }

    [Fact]
    public async Task GetGroups_OnlyReturnsNotDeletedGroups()
    {
        var existingGroup = new Group("Existing group");
        var deletedGroup = new Group("DeletedGroup");

        deletedGroup.Delete();

        _mockGroupRepository.Setup(repo => repo.GetGroups())
            .ReturnsAsync(new List<Group>
            {
                existingGroup,
                deletedGroup
            });

        var expectedGroupList = await _groupService.GetGroups();
        Assert.DoesNotContain(deletedGroup, expectedGroupList);
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromRepository()
    {
        var newGroup = new Group("Team name");
        await _groupService.AddTeamAsync(newGroup);

        _mockGroupRepository.Verify(_ => _.AddTeamAsync(newGroup), Times.Once());
    }

    [Fact]
    public async Task ShouldCallGetAllTeamsMethodFromRepository()
    {
        await _groupService.GetAllTeamsAsync();

        _mockGroupRepository.Verify(_ => _.GetAllTeamsAsync(), Times.Once());
    }

    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileAdding()
    {
        var newGroup = new Group("New Group");
        _mockGroupRepository.Setup(_ => _.AddTeamAsync(newGroup)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _groupService.AddTeamAsync(newGroup));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
    
    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileGettingAll()
    {
        _mockGroupRepository.Setup(_ => _.GetAllTeamsAsync()).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _groupService.GetAllTeamsAsync());
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
}