using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

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

    [Fact]
    public async Task ShouldCallGetAllTeamMemberByTeamAsyncFromService()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        
        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        
        var mockTeamMemberService = new Mock<ITeamMemberService>();
        var logger = Mock.Of<ILogger<TeamMemberController>>();
        var teamMemberController = new TeamMemberController(mockTeamMemberService.Object, logger);

        await teamMemberController.GetAllTeamMembersAsync(teamId);
        
        mockTeamMemberService.Verify(_ => _.GetAllTeamMembersAsync(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnOkWhenEmptyTeamListRetrievedWhileGettingAllTeams()
    {
        var teamId = Guid.NewGuid();
        var member1Id = Guid.NewGuid();
        var teamMember1 = new TeamMember(member1Id, "Name1", "Position1", "email1@gov.com");
        var member2Id = Guid.NewGuid();
        var teamMember2 = new TeamMember(member2Id, "Name2", "Position2", "email2@gov.com");

        var teamMemberList = new List<TeamMember>(){teamMember1, teamMember2};

        var logger = Mock.Of<ILogger<TeamMemberController>>();
        var mockTeamMemberService = new Mock<ITeamMemberService>();
        var teamMemberController = new TeamMemberController(mockTeamMemberService.Object, logger);
        mockTeamMemberService.Setup(service => service.GetAllTeamMembersAsync(teamId))
            .ReturnsAsync(teamMemberList);

        var getAllTeamsAction = await teamMemberController.GetAllTeamMembersAsync(teamId);
        Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        
    }
}