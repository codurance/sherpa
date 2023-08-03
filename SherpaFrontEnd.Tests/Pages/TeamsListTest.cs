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

    [Fact]
    public async Task ShouldShowTitle()
    {
        var page = _testContext.RenderComponent<TeamsList>();
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        
        var allTeamsTitle = page.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
    }
    
    [Fact]
    public async Task ShouldShowAddNewTeamButton()
    {
        var page = _testContext.RenderComponent<TeamsList>();
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        
        var addNewTeamButton = page.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(addNewTeamButton);
    }
}