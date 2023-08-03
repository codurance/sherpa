using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamControllerAcceptanceTest
{
    [Fact]
    public async Task GetTeamsReturnCorrectNumberOfTeamsAndMembersInside()
    {
        var inMemoryTeamRepository = new InMemoryTeamRepository();
        var teamService = new TeamService(inMemoryTeamRepository);
        var teamController = new TeamController(teamService);

        var teamActionResult = await teamController.DeprecatedGetAllTeamsAsync();
        var teamsObjectResult = Assert.IsType<OkObjectResult>(teamActionResult.Result);
        var teams = Assert.IsAssignableFrom<IEnumerable<Team>>(teamsObjectResult.Value);
        Assert.Equal(2, teams.Count());

        var actualFirstTeam = teams.First();
        var firstTeamId = actualFirstTeam.Id;
        var firstTeamName = actualFirstTeam.Name;

        var singleTeamActionResult = await teamController.GetTeamByIdAsync(firstTeamId);
        var singleTeamObjectResult = Assert.IsType<OkObjectResult>(singleTeamActionResult.Result);
        var actualSingleByIdTeam = Assert.IsAssignableFrom<Team>(singleTeamObjectResult.Value);
       
        Assert.Equal(firstTeamId, actualSingleByIdTeam.Id);
        Assert.Equal(firstTeamName, actualSingleByIdTeam.Name);
    }


    [Fact]
    public async Task GetTeamsListIsUpdatedAfterDelete()
    {
        var inMemoryTeamRepository = new InMemoryTeamRepository();
        var teamService = new TeamService(inMemoryTeamRepository);
        var teamController = new TeamController(teamService);

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