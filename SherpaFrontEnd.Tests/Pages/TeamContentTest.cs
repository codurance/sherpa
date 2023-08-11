using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TeamContentTest
{
    private TestContext _testContext;
    private IRenderedComponent<TeamContent> _renderedComponent;
    private Mock<ITeamDataService> _mockTeamService;
    private Mock<IAssessmentsDataService> _mockAssessmentService;

    public TeamContentTest()
    {
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _mockAssessmentService = new Mock<IAssessmentsDataService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _testContext.Services.AddScoped(s => _mockAssessmentService.Object);
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
    public void ShouldDisplayTeamSurveysTabPageContent()
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
        surveyTabPage.Click();
        
        
        teamContentComponent.WaitForAssertion(() => Assert.NotNull(teamContentComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send a new survey"))));
        Assert.NotNull(teamContentComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains("All Surveys")));
        Assert.NotNull(teamContentComponent.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("You don't have any surveys yet")));
    }
}