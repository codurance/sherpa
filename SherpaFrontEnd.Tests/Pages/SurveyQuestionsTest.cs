using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class SurveyQuestionsTest
{
    private Guid _surveyId = Guid.NewGuid();
    private Guid _memberId = Guid.NewGuid();
    private readonly TestContext _ctx;
    private readonly Mock<ISurveyService> _surveyService;
    private readonly FakeNavigationManager _navigationManager;

    public SurveyQuestionsTest()
    {
        _ctx = new TestContext();
        _surveyService
            = new Mock<ISurveyService>();
        _ctx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        _navigationManager = _ctx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task ShouldDisplaySurveyInfoWithoutQuestionsRetrievedFromSurveyService()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            Guid.NewGuid(),
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            "Description",
            Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "Demo Team"),
            templateWithoutQuestions);

        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(survey);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(ComponentParameter.CreateParameter("SurveyId", _surveyId),
                ComponentParameter.CreateParameter("MemberId", _memberId));
        
        var surveyTitleElement = appComponent.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hello team!"));
        Assert.NotNull(surveyTitleElement);
        
        var surveyDescriptionElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Description));
        Assert.NotNull(surveyDescriptionElement);
    }
    
    [Fact]
    public async Task ShouldRedirectToErrorPageIfThereIsAnErrorLoadingTheSurveyContent()
    {
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ThrowsAsync(new Exception());
        
        var appComponent = _ctx.RenderComponent<SurveyQuestions>(ComponentParameter.CreateParameter("SurveyId", _surveyId), ComponentParameter.CreateParameter("MemberId", _memberId));
        
        appComponent.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navigationManager.Uri));
    }
}