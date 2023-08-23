using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamMemberControllerTest
{
    [Fact]
    public async Task ShouldCallAddTeamMemberToTeamAsyncFromService()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        var mockTeamMemberService = new Mock<ITeamMemberService>();
        var logger = Mock.Of<ILogger<TeamMemberController>>();
        var teamMemberController = new TeamMemberController(mockTeamMemberService.Object, logger);

        await teamMemberController.AddTeamMemberToTeamAsync(teamId, teamMember);
        
        mockTeamMemberService.Verify(_ => _.AddTeamMemberToTeamAsync(teamId, teamMember), Times.Once);
    }
}