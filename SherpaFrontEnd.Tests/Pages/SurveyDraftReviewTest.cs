using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Pages.SurveyDraftReview;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class SurveyDraftReviewTest
{
    private Guid _surveyId = Guid.NewGuid();
    private readonly TestContext _ctx;
    private readonly Mock<ISurveyService> _surveyService;
    private readonly Mock<IToastNotificationService> _toastService;
    private readonly FakeNavigationManager _navMan;

    public SurveyDraftReviewTest()
    {
        _ctx = new TestContext();
        _surveyService
            = new Mock<ISurveyService>();
        _toastService = new Mock<IToastNotificationService>();
        _ctx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        _ctx.Services.AddSingleton<IToastNotificationService>(_toastService.Object);
        _navMan = _ctx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task ShouldDisplayDataRetrievedFromSurveyService()
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

        // template
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(survey);

        var appComponent =
            _ctx.RenderComponent<SurveyDraftReview>(ComponentParameter.CreateParameter("SurveyId", _surveyId));
        var templateNameElement =
            appComponent.FindElementByCssSelectorAndTextContent("p", templateWithoutQuestions.Name);
        Assert.NotNull(templateNameElement);

        // title
        var surveyTitleElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Title);
        Assert.NotNull(surveyTitleElement);

        // description
        var surveyDescriptionElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Description);
        Assert.NotNull(surveyDescriptionElement);

        // deadline
        var surveyDeadlineElement =
            appComponent.FindElementByCssSelectorAndTextContent("li", survey.Deadline.Value.ToString("dd/MM/yyyy"));
        Assert.NotNull(surveyDeadlineElement);

        // name of the team
        var teamNameElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Team.Name);
        Assert.NotNull(teamNameElement);

        // button Launch
        var finalLaunchButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        Assert.NotNull(finalLaunchButton);
    }

    [Fact]
    public async Task ShouldRedirectToErrorPageIfThereIsAnErrorLoadingTheSurvey()
    {
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ThrowsAsync(new Exception());

        var appComponent =
            _ctx.RenderComponent<SurveyDraftReview>(ComponentParameter.CreateParameter("SurveyId", _surveyId));

        appComponent.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navMan.Uri));
    }

    [Fact]
    public void ShouldLaunchSurveyAndNavigateToTeamPageWhenLaunchSurveyButtonIsClicked()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId,
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            "Description",
            Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "Demo Team"),
            templateWithoutQuestions);

        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(
            survey);
        var appComponent =
            _ctx.RenderComponent<SurveyDraftReview>(ComponentParameter.CreateParameter("SurveyId", _surveyId));

        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        launchSurveyButton.Click();

        _surveyService.Verify(service => service.LaunchSurvey(survey.Id));

        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/team-content/{survey.Team.Id}/surveys",
                _navMan.Uri));
    }
    
    [Fact]
    public void ShouldSendSuccessToastNotificationWhenLaunchSurveyButtonIsClicked()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId,
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            "Description",
            Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "Demo Team"),
            templateWithoutQuestions);

        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(
            survey);
        var appComponent =
            _ctx.RenderComponent<SurveyDraftReview>(ComponentParameter.CreateParameter("SurveyId", _surveyId));

        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        launchSurveyButton.Click();

        _toastService.Verify(service => service.ShowSuccess("Survey launched successfully"));
    }

    [Fact]
    public void ShouldNavigateToTeamPageAndSendErrorToastNotificationWhenLaunchSurveyServiceThrowsAndError()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId,
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            "Description",
            Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "Demo Team"),
            templateWithoutQuestions);

        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(
            survey);
        _surveyService.Setup(service => service.LaunchSurvey(_surveyId)).ThrowsAsync(new Exception());
        var appComponent =
            _ctx.RenderComponent<SurveyDraftReview>(ComponentParameter.CreateParameter("SurveyId", _surveyId));

        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        launchSurveyButton.Click();

        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/team-content/{survey.Team.Id}/surveys",
                _navMan.Uri));
        
        _toastService.Verify(service => service.ShowError("The survey wasn't launched successfully, please try again"));
    }
}