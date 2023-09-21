using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Infrastructure.Http;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamControllerTest
{

    private readonly Mock<ITeamService> _mockTeamService;
    private readonly TeamController _teamController;

    public TeamControllerTest()
    {
        _mockTeamService = new Mock<ITeamService>();
        var logger = Mock.Of<ILogger<TeamController>>();
        _teamController = new TeamController(_mockTeamService.Object, logger);
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromService()
    {
        const string teamName = "Team name";
        var newTeam = new Team.Domain.Team(teamName);

        await _teamController.AddTeamAsync(newTeam);
        
        _mockTeamService.Verify(_ => _.AddTeamAsync(newTeam), Times.Once());
    }

    [Fact]
    public async Task ShouldRetrieveOkWhenNoProblemsFoundWhileAdding()
    {
        var newTeam = new Team.Domain.Team("New Team");
        Assert.IsType<CreatedResult>(await _teamController.AddTeamAsync(newTeam));
    }
    
    [Fact]
    public async Task ShouldRetrieveErrorIfTeamCannotBeAdded()
    {
        var newTeam = new Team.Domain.Team("New Team");
        var notSuccessfulAdding = new ConnectionToRepositoryUnsuccessfulException("Cannot perform add team function.");
        _mockTeamService.Setup(_ => _.AddTeamAsync(newTeam))
            .ThrowsAsync(notSuccessfulAdding);
            
        var addingResult = await _teamController.AddTeamAsync(newTeam);

        var resultObject = Assert.IsType<ObjectResult>(addingResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }

    [Fact]
    public async Task ShouldCallGetAllTeamsMethodFromService()
    {
        await _teamController.GetAllTeamsAsync();
        
        _mockTeamService.Verify(_ => _.GetAllTeamsAsync(), Times.Once());
    }
    
    [Fact]
    public async Task ShouldReturnOkWhenEmptyTeamListRetrievedWhileGettingAllTeams()
    {
        var emptyTeamsList = new List<Team.Domain.Team>();
        _mockTeamService.Setup(service => service.GetAllTeamsAsync())
            .ReturnsAsync(emptyTeamsList);
        var getAllTeamsAction = await _teamController.GetAllTeamsAsync();
        Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        
    }
    
    [Fact]
    public async Task ShouldReturnOkWhenTeamListRetrievedWhileGettingAllTeams()
    {
        var allTeamsList = new List<Team.Domain.Team>()
        {
            new Team.Domain.Team("Team one"),
            new Team.Domain.Team("Team two")
        };
        _mockTeamService.Setup(service => service.GetAllTeamsAsync())
            .ReturnsAsync(allTeamsList);
        var getAllTeamsAction = await _teamController.GetAllTeamsAsync();
        var okObjectResult = Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        Assert.Equal(allTeamsList, okObjectResult.Value);
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfAllTeamCannotBeRetrieved()
    {
        var notSuccessfulGettingAllTeams = new ConnectionToRepositoryUnsuccessfulException("Cannot perform get all teams function.");
        _mockTeamService.Setup(_ => _.GetAllTeamsAsync())
            .ThrowsAsync(notSuccessfulGettingAllTeams);

        var allTeamsAsync = await _teamController.GetAllTeamsAsync();

        var resultObject = Assert.IsType<ObjectResult>(allTeamsAsync.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }
    
    [Fact]
    public async Task ShouldReturnTeamReturnedByTeamService()
    {
        var teamId = Guid.NewGuid();

        var expectedTeam = new Team.Domain.Team(teamId, "Demo team");
        _mockTeamService.Setup(_ => _.GetTeamByIdAsync(teamId))
            .ReturnsAsync(expectedTeam);

        var actualTeam = await _teamController.GetTeamByIdAsync(teamId);

        var resultObject = Assert.IsType<OkObjectResult>(actualTeam.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        Assert.Equal(expectedTeam, resultObject.Value);
    }
}