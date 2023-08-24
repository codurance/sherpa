using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class TeamMemberServiceTest
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamMemberService _teamMemberService;

    public TeamMemberServiceTest()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _teamMemberService = new TeamMemberService(_mockTeamRepository.Object);

    }

    [Fact]
    public async Task ShouldCallAddTeamMemberToTeamAsyncFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        await _teamMemberService.AddTeamMemberToTeamAsync(teamId, teamMember);
        
        _mockTeamRepository.Verify(_ => _.AddTeamMemberToTeamAsync(teamId, teamMember), Times.Once);
    }

    [Fact]
    public async Task ShouldCallGetAllTeamMembersAsyncFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        await _teamMemberService.GetAllTeamMembersAsync(teamId);
        
        _mockTeamRepository.Verify(_ => _.GetAllTeamMembersAsync(teamId), Times.Once);
    }
    
    [Fact]
    public async Task ShouldGetAllTeamMembersAsAListFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var teamMembers = new List<TeamMember>() { teamMember };

        _mockTeamRepository.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ReturnsAsync(teamMembers);

        var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(teamId);
        Assert.IsType<List<TeamMember>>(allTeamMembers);
        Assert.Contains(teamMember, teamMembers);
        Assert.True(teamMembers.Count() == 1);
    }
    
    [Fact]
    public async Task ShouldGetAnEmptyListOfTeamMembersFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();

        var emptyTeamMembersList = new List<TeamMember>();
        _mockTeamRepository.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ReturnsAsync(emptyTeamMembersList);

        var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(teamId);
        Assert.IsType<List<TeamMember>>(allTeamMembers);
        Assert.True(!allTeamMembers.Any());
    }
    
    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileAddingTeamMembersToATeam()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var teamMembers = new List<TeamMember>() { teamMember };
        var team = new Team(teamId, teamName, teamMembers);
        _mockTeamRepository.Setup(_ => _.AddTeamMemberToTeamAsync(teamId, teamMember)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _teamMemberService.AddTeamMemberToTeamAsync(teamId, teamMember));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
    
    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileGettingAllTeamMembersOfATeam()
    {
        var teamId = Guid.NewGuid();
        _mockTeamRepository.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _teamMemberService.GetAllTeamMembersAsync(teamId));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
}