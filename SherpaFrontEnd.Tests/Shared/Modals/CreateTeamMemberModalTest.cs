using Bunit;
using SherpaFrontEnd.Shared.Modals;

namespace BlazorApp.Tests.Shared.Modals;

public class CreateTeamMemberModalTest
{
    [Fact]
    public void ShouldShowAllFormFieldsTitleAndButtons()
    {
        var ctx = new TestContext();
        var cut = ctx.RenderComponent<CreateTeamMemberModal>(ComponentParameter.CreateParameter("Show", true));
        
        var addMemberTitle = cut.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberTitle);
        
        var addMemberDescription = cut.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add team member by filling in the required information"));
        Assert.NotNull(addMemberDescription);
        
        var fullNameLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = cut.FindAll($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        
        var positionLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Position"));
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.FindAll($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        
        var emailLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Email"));
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.FindAll($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        
        var addMemberButton = cut.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberButton);
        
        var cancelButton = cut.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
        Assert.NotNull(cancelButton);
    }
}