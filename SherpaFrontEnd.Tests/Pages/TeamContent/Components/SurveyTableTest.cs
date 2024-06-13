using Bunit;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages.TeamContent.Components;

namespace BlazorApp.Tests.Pages.Components;

public class SurveyTableTest
{
    private TestContext _testContext;

    public SurveyTableTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void ShouldDisplayCorrectSurveyDataInUserTable()
    {
        var userOne = new User(Guid.NewGuid(), "testUser");
        const string newTeamName = "Team with survey";
        var testTeamId = Guid.NewGuid();
        var testTeam = new Team(testTeamId, newTeamName);

        var testSurvey = new List<Survey>
        {
            new Survey(
                Guid.NewGuid(),
                userOne,
                Status.Draft,
                new DateTime(),
                "title",
                "description",
                Array.Empty<Response>(),
                testTeam,
                new Template("template")
            )
        };

        var surveyTableComponent =
            _testContext.RenderComponent<SurveyTable>(
                ComponentParameter.CreateParameter("Surveys", testSurvey), ComponentParameter.CreateParameter("Flags",
                    new SurveyTableFeatureFlags(true, true, true)));

        var rows = surveyTableComponent.FindAll("tr.bg-white");
        Assert.Equal(testSurvey.Count, rows.Count);

        foreach (var survey in testSurvey)
        {
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Title), null));
            Assert.NotNull(rows.FirstOrDefault(
                element => element.ToMarkup().Contains(survey.Deadline.Value.Date.ToString("dd/MM/yyyy")), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Template.Name), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Coach.Name), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(Status.Draft.ToString()), null));
        }
    }
}