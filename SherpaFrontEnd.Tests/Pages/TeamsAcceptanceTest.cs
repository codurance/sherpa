using Bunit;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class TeamsAcceptanceTest
{
    [Fact]
    void should_be_able_to_create_team()
    {
        var testCtx = new TestContext();
        var teamName = "Test Team";
        var teamsPage = testCtx.RenderComponent<TeamsPage>();

        var createTeamButton = teamsPage.Find("#create-team");
        createTeamButton.Click();

        var teamNameFormInput = teamsPage.Find("#create-team-name");
        teamNameFormInput.Change(teamName);

        var confirmTeamCreationButton = teamsPage.Find("#create-team-confirm");
        confirmTeamCreationButton.Click();

        var actualTeamName = teamsPage.FindAll("h3").FirstOrDefault(title => title.InnerHtml.Contains(teamName));
        Assert.NotNull(actualTeamName);
    }
}