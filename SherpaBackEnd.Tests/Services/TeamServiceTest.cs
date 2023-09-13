using Moq;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Tests.Services;

public class TeamServiceTest
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamService _teamService;

    public TeamServiceTest()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _teamService = new TeamService(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromRepository()
    {
        var newTeam = new Team.Domain.Team("Team name");
        await _teamService.AddTeamAsync(newTeam);

        _mockTeamRepository.Verify(_ => _.AddTeamAsync(newTeam), Times.Once());
    }

    [Fact]
    public async Task ShouldCallGetAllTeamsMethodFromRepository()
    {
        await _teamService.GetAllTeamsAsync();

        _mockTeamRepository.Verify(_ => _.GetAllTeamsAsync(), Times.Once());
    }

    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileAdding()
    {
        var newTeam = new Team.Domain.Team("New Team");
        _mockTeamRepository.Setup(_ => _.AddTeamAsync(newTeam)).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _teamService.AddTeamAsync(newTeam));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
    
    [Fact]
    public async Task ShouldThrowErrorIfConnectionWithRepositoryFailsWhileGettingAll()
    {
        _mockTeamRepository.Setup(_ => _.GetAllTeamsAsync()).ThrowsAsync(new Exception());

        var exceptionThrown = await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () => await _teamService.GetAllTeamsAsync());
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }
    
    [Fact]
    public async Task ShouldReturnTeamGivenByRepoWhenGettingById()
    {
        var teamId = Guid.NewGuid();
        var expectedTeam = new Team.Domain.Team(teamId, "Demo team");
        _mockTeamRepository.Setup(_ => _.GetTeamByIdAsync(teamId)).ReturnsAsync(expectedTeam);

        var actualTeam = await _teamService.GetTeamByIdAsync(teamId);

        Assert.Equal(expectedTeam, actualTeam);
    }
}