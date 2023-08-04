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
    [Fact]
    public void ShouldRenderAllInputsAndButtons()
    {
        var testCtx = new TestContext();
        var component = testCtx.RenderComponent<CreateTeamOffcanvas>();
        
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
        var testCtx = new TestContext();
        var guidService = new Mock<IGuidService>();
        testCtx.Services.AddSingleton<IGuidService>(guidService.Object);
        
        var teamId = Guid.NewGuid();
        guidService.Setup(s => s.GenerateRandomGuid()).Returns(teamId);
        
        var teamService = new Mock<ITeamDataService>();
        testCtx.Services.AddSingleton<ITeamDataService>(teamService.Object);

        var navMan = testCtx.Services.GetRequiredService<FakeNavigationManager>();

        var component = testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var teamNameLabel = component.FindAll("label").FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        const string teamName = "Demo team";
        teamNameInput.Change(teamName);
        
        var confirmButton = component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();
        
        teamService.Verify(service => service.AddTeam(It.IsAny<Team>()));
        Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", navMan.Uri);
    }
}