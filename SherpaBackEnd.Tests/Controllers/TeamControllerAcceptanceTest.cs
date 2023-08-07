using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamControllerAcceptanceTest
{
    [Fact]
    public async Task GetTeamsListIsUpdatedAfterDelete()
    {
        var inMemoryTeamRepository = new InMemoryTeamRepository();
        var teamService = new TeamService(inMemoryTeamRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var teamController = new TeamController(teamService, logger);

        var teamsActionResult = await teamController.DeprecatedGetAllTeamsAsync();
        var teamsObjectResult = Assert.IsType<OkObjectResult>(teamsActionResult.Result);
        var teams = Assert.IsAssignableFrom<IEnumerable<Team>>(teamsObjectResult.Value).ToList();
        Assert.Equal(2, teams.Count());

        var teamToDelete = teams.First();
        await teamController.DeleteTeamByIdAsync(teamToDelete.Id);

        var teamsAfterDeleteActionResult = await teamController.DeprecatedGetAllTeamsAsync();
        var teamsAfterDeleteObjectResult = Assert.IsType<OkObjectResult>(teamsAfterDeleteActionResult.Result);
        var teamsAfterDelete = Assert.IsAssignableFrom<IEnumerable<Team>>(teamsAfterDeleteObjectResult.Value).ToList();
        Assert.Single(teamsAfterDelete);

        Assert.DoesNotContain(teamsAfterDelete, g => g.Id == teamToDelete.Id);
    }
}