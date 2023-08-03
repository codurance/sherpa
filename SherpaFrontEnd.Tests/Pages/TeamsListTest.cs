using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TeamListTest
{

    private TestContext _testContext;
    private IRenderedComponent<TeamsList> _renderedComponent;
    private Mock<ITeamDataService> _mockTeamService;

    public TeamListTest()
    {
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
    }

    [Fact]
    public void AssertThatListOfTeamsIsRendered()
    {
        var team = new Team
        {
            Name = "Team A",
            Id = Guid.NewGuid()
        };
        
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>{team});
        _renderedComponent = _testContext.RenderComponent<TeamsList>();
        
        Assert.Equal(team.Name, _renderedComponent.Instance.Teams![0].Name);
    }
}