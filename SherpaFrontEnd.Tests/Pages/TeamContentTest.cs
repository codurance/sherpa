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
}