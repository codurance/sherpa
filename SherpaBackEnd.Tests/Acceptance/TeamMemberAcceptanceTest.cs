using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamMembersAcceptanceTest
{
    [Fact]
    public async Task ShouldBeAbleToAddATeamMemberToAnExistingTeamAndGetUpdatedListOfTeamMembers()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        var newTeam = new Team( teamId,teamName);
        var teamsList = new List<Team>(){newTeam};
        var inMemoryTeamRepository = new InMemoryTeamRepository(teamsList);
        var teamMemberService = new TeamMemberService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);
    }
}