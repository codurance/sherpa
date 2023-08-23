using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class TeamMemberServiceTest
{
    [Fact]
    public async Task ShouldCallAddTeamMemberToTeamAsyncFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        var mockTeamRepository = new Mock<ITeamRepository>();
        var teamMemberService = new TeamMemberService(mockTeamRepository.Object);

        await teamMemberService.AddTeamMemberToTeamAsync(teamId, teamMember);
        
        mockTeamRepository.Verify(_ => _.AddTeamMemberToTeamAsync(teamId, teamMember), Times.Once);
    }

    [Fact]
    public async Task ShouldCallGetTeamMembersToTeamAsyncFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        var mockTeamRepository = new Mock<ITeamRepository>();
        var teamMemberService = new TeamMemberService(mockTeamRepository.Object);

        await teamMemberService.GetAllTeamMembersAsync(teamId, teamMember);
        
        mockTeamRepository.Verify(_ => _.GetAllTeamMembersAsync(teamId, teamMember), Times.Once);
    }
}