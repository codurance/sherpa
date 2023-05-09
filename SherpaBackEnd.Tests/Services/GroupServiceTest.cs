using Moq;
using SherpaBackEnd.Dtos;
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
    
}