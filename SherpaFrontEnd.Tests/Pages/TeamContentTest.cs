using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Pages;

public class TeamContentTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private TestContext _testContext;
    private IRenderedComponent<TeamContent> _renderedComponent;
    private Mock<ITeamDataService> _mockTeamService;
    private Mock<IAssessmentsDataService> _mockAssessmentService;
    private Mock<ISurveyService> _mockSurveyService;

    public TeamContentTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _mockAssessmentService = new Mock<IAssessmentsDataService>();
        _mockSurveyService = new Mock<ISurveyService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _testContext.Services.AddScoped(s => _mockAssessmentService.Object);
        _testContext.Services.AddSingleton<ISurveyService>(_mockSurveyService.Object);
    }

    [Fact]
    public void SingleTeamIsRendered()
    {
        const string teamName = "Demo team";
        var team = new Team
        {
            Name = teamName,
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);

        var teamDetailsPage = _testContext.RenderComponent<TeamContent>();
        var teamNameElement = teamDetailsPage.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
        var analysisTab = teamDetailsPage.FindAll("li").FirstOrDefault(element => element.InnerHtml.Contains("Analysis"));
        var sendNewSurveyTeam = teamDetailsPage.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send a new survey"));
        
        Assert.NotNull(teamNameElement);
        Assert.NotNull(analysisTab);
        Assert.NotNull(sendNewSurveyTeam);
    }

    [Fact]
    public void ShouldDisplayTeamSurveysTabPage()
    {
        const string teamName = "Demo team";
        var team = new Team
        {
            Name = teamName,
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent = _testContext.RenderComponent<TeamContent>();
        
        var surveyTabPage = teamContentComponent.FindAll("a:not(a[href])").FirstOrDefault(element => element.InnerHtml.Contains("Surveys"));
        
        Assert.NotNull(surveyTabPage);
    }
    
    [Fact]
    public void ShouldDisplayTeamSurveysTabPageContentWhenEmptySurveysList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };
        
        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>());

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent = _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));
        
        var surveyTabPage = teamContentComponent.FindAll("a:not(a[href])").FirstOrDefault(element => element.InnerHtml.Contains("Surveys"));
        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();
        
        
        teamContentComponent.WaitForAssertion(() => Assert.NotNull(teamContentComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send first survey"))));
        Assert.NotNull(teamContentComponent.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("Let's begin the journey towards a stronger, more effective team!")));
        Assert.NotNull(teamContentComponent.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("You don’t have any surveys yet")));
    }
    
    [Fact]
    public void ShouldDisplayTeamSurveysTabPageContentWhenNonEmptyList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };
        var userOne = new User(Guid.NewGuid(), "user");

        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>(){new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
            Array.Empty<Response>(), team, new Template("template"))});
            
        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent = _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));
        
        var surveyTabPage = teamContentComponent.FindAll("a:not(a[href])").FirstOrDefault(element => element.InnerHtml.Contains("Surveys"));
        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();
        
        teamContentComponent.WaitForAssertion(() => Assert.NotNull(teamContentComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains("All Surveys"))));
        Assert.NotNull(teamContentComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send new survey")));
    }

    [Fact]
    public void ShouldDisplayCorrectHeadingsForUserTableInTeamSurveysTabPageContentWhenNonEmptyList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };
        var userOne = new User(Guid.NewGuid(), "user");

        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>(){new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
            Array.Empty<Response>(), team, new Template("template"))});
            
        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent = _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));
        
        var surveyTabPage = teamContentComponent.FindAll("a:not(a[href])").FirstOrDefault(element => element.InnerHtml.Contains("Surveys"));
        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();

        var surveyName = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Survey name"));
        var template = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Template"));
        var coach = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Coach"));
        var deadline = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Deadline"));
        var participants = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Participants"));
        var status = teamContentComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Status"));
        
        teamContentComponent.WaitForAssertion(() => Assert.NotNull(teamContentComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains("All Surveys"))));
        Assert.NotNull(teamContentComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send new survey")));
        Assert.NotNull(surveyName);
        Assert.NotNull(template);
        Assert.NotNull(coach);
        Assert.NotNull(deadline);
        Assert.NotNull(participants);
        Assert.NotNull(status);
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

            var surveyTableComponent = _testContext.RenderComponent<SurveyTable>(ComponentParameter.CreateParameter("Surveys", testSurvey));

                var rows = surveyTableComponent.FindAll("tr.bg-white");
                Assert.Equal(testSurvey.Count, rows.Count);

                foreach (var survey in testSurvey)
                {
                    Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Title), null));
                    Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Deadline.Value.Date.ToString("dd/MM/yyyy")), null));
                    Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Template.Name), null));
                    Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Coach.Name), null));
                    Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(Status.Draft.ToString()), null));
                }
        }
}