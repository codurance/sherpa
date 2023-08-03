using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamControllerTest
{

    private readonly Mock<ITeamService> _mockTeamService;
    private readonly TeamController _teamController;

    public TeamControllerTest()
    {
        _mockTeamService = new Mock<ITeamService>();
        _teamController = new TeamController(_mockTeamService.Object);
    }

    [Fact]
    public async Task GetTeams_RepoReturnsEmptyList_NotFoundExpected()
    {
        _mockTeamService.Setup(service => service.DeprecatedGetAllTeamsAsync())
            .ReturnsAsync(new List<Team>());

        var actionResult = await _teamController.DeprecatedGetAllTeamsAsync();
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetTeams_RepoReturnsList_OkExpected()
    {
        _mockTeamService.Setup(repo => repo.DeprecatedGetAllTeamsAsync())
            .ReturnsAsync(new List<Team>{new("Team A"),new("Team B")});

        var actionResult = await _teamController.DeprecatedGetAllTeamsAsync();
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var teams = Assert.IsAssignableFrom<IEnumerable<Team>>(objectResult.Value);
        Assert.Equal(2,teams.Count());
    }

    [Fact]
    public async Task GetTeams_RepoThrowsError_ServerErrorExpected()
    {
        var dbException = new ConnectionToRepositoryUnsuccessfulException("Couldn't connect to the database");
        _mockTeamService.Setup(repo => repo.DeprecatedGetAllTeamsAsync())
            .ThrowsAsync(dbException);
        var actionResult = await _teamController.DeprecatedGetAllTeamsAsync();

        var objectResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        
    }

    [Fact]
    public async Task GetTeamById_RepoReturnsEmptyTeam_OkExpected()
    {
        var expectedTeam = new Team("Team");
        var guid = expectedTeam.Id;
        
        _mockTeamService.Setup(m => m.GetTeamByIdAsync(guid)).ReturnsAsync(expectedTeam);
        var actionResult = await _teamController.GetTeamByIdAsync(guid);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualTeam = Assert.IsAssignableFrom<Team>(okObjectResult.Value);
        Assert.Empty(actualTeam.Members);
    }


    [Fact]
    public async Task GetTeamById_RepoDoesntReturnTeam_NotFoundExpected()
    {
        _mockTeamService.Setup(m => m.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Team)null);
        
        var actionResult = await _teamController.GetTeamByIdAsync(new Guid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewTeam_OkResultExpected()
    {
        var expectedTeam = new Team("New team");

        _mockTeamService.Setup(m => m.DeprecatedAddTeamAsync(It.IsAny<Team>()));

        var actionResult = await _teamController.DeprecatedAddTeamAsync(expectedTeam);
        Assert.IsType<OkResult>(actionResult.Result);
    }

    [Fact]
    public async Task AddNewTeamWithoutName_BadRequestResultExpected()
    {
        var expectedTeam = new Team("");

        var actionResult = await _teamController.DeprecatedAddTeamAsync(expectedTeam);
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task DeleteTeam_TeamDoesNotExist_ExpectedNotFound()
    {
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Team)null);
        
        var actionResult = await _teamController.DeleteTeamByIdAsync(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteTeam_TeamExists_ExpectedOkResult()
    {
        var team = new Team("Deleting Team");
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(team);
        
        var actionResult = await _teamController.DeleteTeamByIdAsync(team.Id);
        Assert.IsType<OkResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteTeam_TeamExists_VerifyInteractionWithRepository()
    {
        var team = new Team("Deleting Team");
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(team.Id))
            .ReturnsAsync(team);
        
        await _teamController.DeleteTeamByIdAsync(team.Id);
        _mockTeamService.Verify(repo => repo.GetTeamByIdAsync(team.Id));
        Assert.True(team.IsDeleted);
        _mockTeamService.Verify(repo => repo.UpdateTeamByIdAsync(team));
    }
    
    [Fact]
    public async Task UpdateTeam_TeamDoesNotExist_ExpectedNotFound()
    {
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Team)null);
        
        var actionResult = await _teamController.UpdateTeamAsync(Guid.NewGuid(), new Team("name"));
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task UpdateTeam_TeamExists_AssertEntireTeamIsPassed()
    {
        var team = new Team("Team A");
        team.Members = new List<TeamMember>
        {
            new ("Name A", "lastName A", "position A", "e1@e.com"),
            new ("Name B", "lastName B", "position B", "e2@e.com")
        };
        
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(team);

        var members = team.Members.ToList();
        members.Add(new TeamMember("Name C", "Lastname C", "position C", "e3@e.com"));
        team.Members = members;
        
        await _teamController.UpdateTeamAsync(team.Id,team);
     
        _mockTeamService.Verify(repo => repo.UpdateTeamByIdAsync(It.Is<Team>(updatedTeam => updatedTeam.Members.ToList().Count.Equals(3))));
    }
    
    [Fact]
    public async Task UpdateTeam_TeamExists_AssertRightIdIsPassed()
    {
        var team = new Team("Team A");
        team.Members = new List<TeamMember>
        {
            new ("Name A", "lastName A", "position A", "e1@e.com"),
            new ("Name B", "lastName B", "position B", "e2@e.com")
        };
        
        _mockTeamService.Setup(repo => repo.GetTeamByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(team);

        team.Name = "updated name";
        
        await _teamController.UpdateTeamAsync(team.Id,team);
     
        _mockTeamService.Verify(repo => repo.UpdateTeamByIdAsync(It.Is<Team>(updatedTeam => updatedTeam.Id.Equals(team.Id))));
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromService()
    {
        const string teamName = "Team name";
        var newTeam = new Team(teamName);

        await _teamController.AddTeamAsync(newTeam);
        
        _mockTeamService.Verify(_ => _.AddTeamAsync(newTeam), Times.Once());
    }

    [Fact]
    public async Task ShouldRetrieveOkWhenNoProblemsFoundWhileAdding()
    {
        var newTeam = new Team("New Team");
        Assert.IsType<CreatedResult>(await _teamController.AddTeamAsync(newTeam));
    }
    
    [Fact]
    public async Task ShouldRetrieveErrorIfTeamCannotBeAdded()
    {
        var newTeam = new Team("New Team");
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
        var emptyTeamsList = new List<Team>();
        _mockTeamService.Setup(service => service.GetAllTeamsAsync())
            .ReturnsAsync(emptyTeamsList);
        var getAllTeamsAction = await _teamController.GetAllTeamsAsync();
        Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        
    }
    
    [Fact]
    public async Task ShouldReturnOkWhenTeamListRetrievedWhileGettingAllTeams()
    {
        var allTeamsList = new List<Team>()
        {
            new Team("Team one"),
            new Team("Team two")
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
}