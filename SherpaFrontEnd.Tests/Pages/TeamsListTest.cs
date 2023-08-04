using AngleSharp.Common;
using BlazorApp.Tests.Acceptance;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Newtonsoft.Json;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Pages;

public class TeamListTest
{
    private readonly TestContext _testContext;
    private readonly Mock<ITeamDataService> _mockTeamService;
    private readonly Mock<IGuidService> _mockGuidService;

    public TeamListTest()
    {
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _mockGuidService = new Mock<IGuidService>();
        _testContext.Services.AddScoped(s => _mockGuidService.Object);
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

        var allTeamsTitle = page.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
    }

    [Fact]
    public async Task ShouldShowAddNewTeamButton()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var page = _testContext.RenderComponent<TeamsList>();

        var addNewTeamButton = page.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(addNewTeamButton);
    }

    [Fact]
    public async Task ShouldShowTeamsNameIfTheyExist()
    {
        const string teamName = "Team name";
        _mockTeamService.Setup(m => m.GetAllTeams())
            .ReturnsAsync(new List<Team>() { new Team(Guid.NewGuid(), teamName) });
        var page = _testContext.RenderComponent<TeamsList>();

        var teamNameElement = page.FindAll("h5").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
        Assert.NotNull(teamNameElement);
    }

    [Fact]
    public async Task ShouldCallShowOffScreenJsFunctionWhenClickingCreateNewTeamButton()
    {
        _mockTeamService.Setup(m => m.GetAllTeams()).ReturnsAsync(new List<Team>());
        var jsRuntimeInvocationHandler = _testContext.JSInterop.SetupVoid("showOffCanvas").SetVoidResult();

        var page = _testContext.RenderComponent<TeamsList>();

        var createNewTeamButton = page.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        var jsRuntimeInvocation = _testContext.JSInterop.Invocations.GetItemByIndex(0);

        Assert.Equal("showOffCanvas", jsRuntimeInvocation.Identifier);
        Assert.Contains("create-new-team-form", jsRuntimeInvocation.Arguments);
    }
}