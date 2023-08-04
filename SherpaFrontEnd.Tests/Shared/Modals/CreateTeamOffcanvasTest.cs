using Bunit;
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
}