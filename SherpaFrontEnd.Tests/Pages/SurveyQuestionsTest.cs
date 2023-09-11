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
    public void ShouldCallServiceMethodsWhenLoadingSurveyQuestions()
    {
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyId", _surveyId),
                ComponentParameter.CreateParameter("MemberId", _memberId)
            );
        
        _surveyService.Verify(_ => _.GetSurveyWithoutQuestionsById(_surveyId), Times.Once);
        _surveyService.Verify(_ => _.GetSurveyQuestionsBySurveyId(_surveyId), Times.Once);
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
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyId", _surveyId),
                ComponentParameter.CreateParameter("MemberId", _memberId)
            );
        
        var surveyTitleElement = appComponent.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Title));
        Assert.NotNull(surveyTitleElement);

        var surveyDescriptionElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Description));
        Assert.NotNull(surveyDescriptionElement);
    }
    
    [Fact]
    public async Task ShouldDisplaySingleSurveyQuestionRetrievedFromSurveyService()
    {
        var QuestionInSpanish = "Question in spanish";
        var QuestionInEnglish = "Question in english";
        var ResponseSpanish1 = "SPA_1";
        var ResponseSpanish2 = "SPA_2";
        var ResponseSpanish3 = "SPA_3";
        var ResponseEnglish1 = "ENG_1";
        var ResponseEnglish2 = "ENG_2";
        var ResponseEnglish3 = "ENG_3";
        var Position = 1;
        var Reverse = false;

        var question = new Question(new Dictionary<string, string>()
            {
                { Languages.SPANISH, QuestionInSpanish },
                { Languages.ENGLISH, QuestionInEnglish },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                }
            }, Reverse,
            HackmanComponent.INTERPERSONAL_PEER_COACHING,
            HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position);

        var questions = new List<Question>() { question };

        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyId", _surveyId),
                ComponentParameter.CreateParameter("MemberId", _memberId)
            );
        
        var questionSpanishStatement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(questions[0].Statement[Languages.ENGLISH]));
        Assert.NotNull(questionSpanishStatement);

        var amountOfAvailableResponses = appComponent.FindAll("input[type=radio]").Count;
        Assert.Equal(questions[0].Responses[Languages.ENGLISH].Length, amountOfAvailableResponses);
    }
    
    [Fact]
    public async Task ShouldDisplayMultipleSurveyQuestionRetrievedFromSurveyService()
    {
        var ResponseSpanish1 = "SPA_1";
        var ResponseSpanish2 = "SPA_2";
        var ResponseSpanish3 = "SPA_3";
        var ResponseEnglish1 = "ENG_1";
        var ResponseEnglish2 = "ENG_2";
        var ResponseEnglish3 = "ENG_3";
        var Position = 1;
        var Reverse = false;

        var firstQuestion = new Question(new Dictionary<string, string>()
            {
                { Languages.SPANISH, "Primera pregunta en espanol" },
                { Languages.ENGLISH, "First Question in english" },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                }
            }, Reverse,
            HackmanComponent.INTERPERSONAL_PEER_COACHING,
            HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position);
        
        var secondQuestion = new Question(new Dictionary<string, string>()
            {
                { Languages.SPANISH, "Segunda pregunta en espanol" },
                { Languages.ENGLISH, "Second Question in english" },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                }
            }, Reverse,
            HackmanComponent.INTERPERSONAL_PEER_COACHING,
            HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position);

        var questions = new List<Question>() { firstQuestion, secondQuestion };

        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyId", _surveyId),
                ComponentParameter.CreateParameter("MemberId", _memberId)
            );
        
        var firstQuestionSpanishStatement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(questions[0].Statement[Languages.ENGLISH]));
        Assert.NotNull(firstQuestionSpanishStatement);
        
        var secondQuestionSpanishStatement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(questions[1].Statement[Languages.ENGLISH]));
        Assert.NotNull(secondQuestionSpanishStatement);
    }

    [Fact]
    public async Task ShouldRedirectToErrorPageIfThereIsAnErrorLoadingTheSurveyContent()
    {
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ThrowsAsync(new Exception());

        var appComponent = _ctx.RenderComponent<SurveyQuestions>(
            ComponentParameter.CreateParameter("SurveyId", _surveyId),
            ComponentParameter.CreateParameter("MemberId", _memberId));

        appComponent.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navigationManager.Uri));
    }
}