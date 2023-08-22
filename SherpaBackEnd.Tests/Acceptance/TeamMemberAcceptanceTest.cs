using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamMemberAcceptanceTest
{
    [Fact]
    public async Task ShouldBeAbleToAddATeamMemberToAnExistingTeamAndGetUpdatedListOfTeamMembers()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        var newTeam = new Team(teamId, teamName);
        var memberId = Guid.NewGuid();
        var newTeamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");

        var teamsList = new List<Team>() { newTeam };
        var inMemoryTeamRepository = new InMemoryTeamRepository(teamsList);
        var teamMemberService = new TeamMemberService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamMemberController = new TeamMemberController(teamMemberService, logger);

        await teamMemberController.AddTeamMemberToTeamAsync(teamId, newTeamMember);

        var currentTeamMembers = await teamMemberController.GetAllTeamMembersAsync();
        
        var okObjectResult = Assert.IsType<OkObjectResult>(currentTeamMembers.Result);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        var teamMembersList = Assert.IsType<List<TeamMember>>(okObjectResult.Value);
        Assert.Contains(newTeamMember, teamMembersList); 
    }
}