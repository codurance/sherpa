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
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var page = _testContext.RenderComponent<TeamsList>();
        
        var allTeamsTitle = page.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
    }
    
    [Fact]
    public async Task ShouldShowAddNewTeamButton()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var page = _testContext.RenderComponent<TeamsList>();
        
        var addNewTeamButton = page.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(addNewTeamButton);
    }
    
    [Fact]
    public async Task ShouldShowTeamsNameIfTheyExist()
    {
        const string teamName = "Team name";
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>(){new Team(Guid.NewGuid(), teamName)});
        var page = _testContext.RenderComponent<TeamsList>();
        
        var teamNameElement = page.FindAll("h5").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
        Assert.NotNull(teamNameElement);
    }
}