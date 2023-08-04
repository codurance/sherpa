using BlazorApp.Tests.Acceptance;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared.Modals;

namespace BlazorApp.Tests.Shared.Modals;

public class CreateTeamOffcanvasTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<IGuidService> _guidService;
    private readonly Mock<ITeamDataService> _teamService;
    private readonly FakeNavigationManager _navMan;
    private readonly Guid _teamId;

    public CreateTeamOffcanvasTest()
    {
        _testCtx = new TestContext();
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        
        _teamId = Guid.NewGuid();
        _guidService.Setup(s => s.GenerateRandomGuid()).Returns(_teamId);
        
        _teamService = new Mock<ITeamDataService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamService.Object);

        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }
    

    [Fact]
    public void ShouldRenderAllInputsAndButtons()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var createNewTeamTitle = component.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamTitle);
        
        var teamNameLabel = component.FindAll("label").FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.FindAll($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        
        var continueButton = component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        var cancelButton = component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
        
        Assert.NotNull(continueButton);
        Assert.NotNull(cancelButton);
    }

    [Fact]
    public async Task ShouldCallServiceAndRedirectToCreatedTeamPage()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var teamNameLabel = component.FindAll("label").FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        const string teamName = "Demo team";
        teamNameInput.Change(teamName);
        
        var confirmButton = component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();
        
        _teamService.Verify(service => service.AddTeam(It.IsAny<Team>()));
        Assert.Equal($"http://localhost/team-content/{_teamId.ToString()}", _navMan.Uri);
    }
}