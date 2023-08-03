using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamsAcceptanceTest
{
    [Fact]
    public async Task ShouldBeAbleToCreateTeamAndGetUpdatedListOfTeamsWithNewOne()
    {
        var emptyTeamsList = new List<Team>();
        var inMemoryTeamRepository = new InMemoryTeamRepository(emptyTeamsList);
        var teamService = new TeamService(inMemoryTeamRepository);
        var teamController = new TeamController(teamService);

        const string teamName = "New team";
        var newTeam = new Team(teamName);

        await teamController.AddTeamAsync(newTeam);

        var actualTeams = await teamController.GetAllTeamsAsync();
        var okObjectResult = Assert.IsType<OkObjectResult>(actualTeams.Result);
        Assert.Equal(emptyTeamsList, okObjectResult.Value);
    }
}