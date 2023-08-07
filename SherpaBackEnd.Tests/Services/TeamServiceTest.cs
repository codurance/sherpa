using Moq;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

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
    public async Task GetTeams_OnlyReturnsNotDeletedTeams()
    {
        var existingTeam = new Team("Existing team");
        var deletedTeam = new Team("Deleted team");

        deletedTeam.Delete();

        _mockTeamRepository.Setup(repo => repo.DeprecatedGetAllTeams())
            .ReturnsAsync(new List<Team>
            {
                existingTeam,
                deletedTeam
            });

        var expectedTeamsList = await _teamService.DeprecatedGetAllTeamsAsync();
        Assert.DoesNotContain(deletedTeam, expectedTeamsList);
    }

    [Fact]
    public async Task ShouldCallAddTeamMethodFromRepository()
    {
        var newTeam = new Team("Team name");
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
        var newTeam = new Team("New Team");
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
        var expectedTeam = new Team(teamId, "Demo team");
        _mockTeamRepository.Setup(_ => _.GetTeamByIdAsync(teamId)).ReturnsAsync(expectedTeam);

        var actualTeam = await _teamService.GetTeamByIdAsync(teamId);

        Assert.Equal(expectedTeam, actualTeam);
    }
}