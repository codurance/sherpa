using AngleSharp.Common;
using BlazorApp.Tests.Acceptance;

using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Newtonsoft.Json;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Pages.TeamList;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Pages;

public class TeamListTest
{
    private readonly TestContext _testContext;
    private readonly Mock<ITeamDataService> _mockTeamService;
    private readonly Mock<IGuidService> _mockGuidService;
    private readonly FakeNavigationManager _navMan;
    private readonly Mock<IToastNotificationService> _toastService;

    public TeamListTest()
    {
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _mockGuidService = new Mock<IGuidService>();
        _testContext.Services.AddScoped(s => _mockGuidService.Object);
        _toastService = new Mock<IToastNotificationService>();
        _testContext.Services.AddSingleton<IToastNotificationService>(_toastService.Object);
        _navMan = _testContext.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public void AssertThatListOfTeamsIsRendered()
    {
        var team = new Team
        {
            Name = "Team A",
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team> { team });
        var renderedComponent = _testContext.RenderComponent<TeamsList>();

        Assert.Equal(team.Name, renderedComponent.Instance.Teams![0].Name);
    }

    [Fact]
    public async Task ShouldShowTitle()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var page = _testContext.RenderComponent<TeamsList>();

        var allTeamsTitle = page.FindElementByCssSelectorAndTextContent("h1,h2,h3", "All teams");
        Assert.NotNull(allTeamsTitle);
    }

    [Fact]
    public async Task ShouldShowAddNewTeamButton()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var page = _testContext.RenderComponent<TeamsList>();

        var addNewTeamButton = page.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(addNewTeamButton);
    }

    [Fact]
    public async Task ShouldShowTeamsNameIfTheyExist()
    {
        const string teamName = "Team name";
        _mockTeamService.Setup(m => m.GetAllTeams())
            .ReturnsAsync(new List<Team>() { new Team(Guid.NewGuid(), teamName) });
        var page = _testContext.RenderComponent<TeamsList>();

        var teamNameElement = page.FindElementByCssSelectorAndTextContent("h2", teamName);
        Assert.NotNull(teamNameElement);
    }

    [Fact]
    public async Task ShouldCallShowOffScreenJsFunctionWhenClickingCreateNewTeamButton()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        _testContext.JSInterop.SetupVoid("showOffCanvas").SetVoidResult();

        var page = _testContext.RenderComponent<TeamsList>();

        var createNewTeamButton = page.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        var jsRuntimeInvocation = _testContext.JSInterop.Invocations.GetItemByIndex(0);

        Assert.Equal("showOffCanvas", jsRuntimeInvocation.Identifier);
        Assert.Contains("create-new-team-form", jsRuntimeInvocation.Arguments);
    }

    [Fact]
    public async Task ShouldBeAbleToClickOnATeamAndNavigateToItsOwnPage()
    {
        const string teamName = "Team Name";
        var teamId = Guid.NewGuid();
        var newTeam = new Team(teamId, teamName);
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>(){newTeam});
        _mockTeamService.Setup(m => m.GetTeamById(teamId)).ReturnsAsync(newTeam);
        var page = _testContext.RenderComponent<TeamsList>();
        
        var existingTeamNameElement = page.FindElementByCssSelectorAndTextContent("h2", teamName);
        Assert.NotNull(existingTeamNameElement);
        
        existingTeamNameElement.Click();
        
        Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", _navMan.Uri);
    }
    
    [Fact]
    public async Task ShouldRedirectToErrorPageIfServiceThrowsWhenGettingTeams()
    {
        const string teamName = "Team Name";
        var teamId = Guid.NewGuid();
        var newTeam = new Team(teamId, teamName);
        _mockTeamService.Setup(m => m.GetAllTeams()).ThrowsAsync(new Exception());
        
        _testContext.RenderComponent<TeamsList>();
        
        Assert.Equal($"http://localhost/error", _navMan.Uri);
    }
}