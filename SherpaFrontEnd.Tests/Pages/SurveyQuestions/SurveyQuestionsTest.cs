using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages.SurveyQuestions;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class SurveyQuestionsTest
{
    private Guid _surveyId = Guid.NewGuid();
    private Guid _memberId = Guid.NewGuid();
    private Guid _surveyNotificationId = Guid.NewGuid();
    private readonly TestContext _ctx;
    private readonly Mock<ISurveyService> _surveyService;
    private readonly Mock<ICachedResponseService> _cachedResponseService;
    private readonly FakeNavigationManager _navigationManager;

    public SurveyQuestionsTest()
    {
        _ctx = new TestContext();
        _surveyService
            = new Mock<ISurveyService>();
        _cachedResponseService = new Mock<ICachedResponseService>();
        _ctx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        _ctx.Services.AddSingleton<ICachedResponseService>(_cachedResponseService.Object);
        _navigationManager = _ctx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public void ShouldCallServiceMethodsWhenLoadingSurveyQuestions()
    {
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _ctx.RenderComponent<SurveyQuestions>(
            ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
        );

        _surveyService.Verify(service => service.GetSurveyNotificationById(_surveyNotificationId), Times.Once);
        _surveyService.Verify(service => service.GetSurveyWithoutQuestionsById(_surveyId), Times.Once);
        _surveyService.Verify(service => service.GetSurveyQuestionsBySurveyId(_surveyId), Times.Once);
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

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ReturnsAsync(survey);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var surveyTitleElement = appComponent.FindElementByCssSelectorAndTextContent("h2", survey.Title);
        Assert.NotNull(surveyTitleElement);

        var surveyDescriptionElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Description);
        Assert.NotNull(surveyDescriptionElement);
    }

    [Fact]
    public async Task ShouldDisplaySingleSurveyQuestionRetrievedFromSurveyService()
    {
        SetupQuestions(out var question, out var secondQuestion);

        var questions = new List<Question>() { question };

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var questionSpanishStatement =
            appComponent.FindElementByCssSelectorAndTextContent("legend", questions[0].Statement[Languages.ENGLISH]);
        Assert.NotNull(questionSpanishStatement);

        var amountOfAvailableResponses = appComponent.FindAll("input[type=radio]").Count;
        Assert.Equal(questions[0].Responses[Languages.ENGLISH].Length, amountOfAvailableResponses);

        var submitButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Submit");
        Assert.NotNull(submitButton);
    }

    [Fact]
    public async Task ShouldDisplayMultipleSurveyQuestionRetrievedFromSurveyService()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);
        
        var questions = new List<Question>() { firstQuestion, secondQuestion };

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var firstQuestionSpanishStatement =
            appComponent.FindElementByCssSelectorAndTextContent("legend", questions[0].Statement[Languages.ENGLISH]);
        Assert.NotNull(firstQuestionSpanishStatement);

        var secondQuestionSpanishStatement =
            appComponent.FindElementByCssSelectorAndTextContent("legend", questions[1].Statement[Languages.ENGLISH]);
        Assert.NotNull(secondQuestionSpanishStatement);

        var submitButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Submit");
        Assert.NotNull(submitButton);
    }

    [Fact]
    public async Task ShouldRedirectToErrorPageIfThereIsAnErrorLoadingTheSurveyContent()
    {
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyWithoutQuestionsById(_surveyId)).ThrowsAsync(new Exception());

        var appComponent = _ctx.RenderComponent<SurveyQuestions>(
            ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId));

        appComponent.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navigationManager.Uri));
    }

    [Fact]
    public async Task ShouldSendAnswerSurveyDtoToSurveyServiceAndShowThankYouMessageInEnglishOnSubmit()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);

        var questions = new List<Question>() { firstQuestion, secondQuestion };

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var survey = appComponent.Find("form[id='survey']");

        var question1 = 1;
        var question2 = 2;
        var question1AnswerIndex = 1;
        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][question1AnswerIndex];
        var question2AnswerIndex = 1;
        var question2Answer = secondQuestion.Responses[Languages.ENGLISH][question2AnswerIndex];

        var question1AnswerElement = appComponent.Find($"input[id='{question1}-{question1AnswerIndex}']");
        question1AnswerElement.Change(new ChangeEventArgs());
        var question2AnswerElement = appComponent.Find($"input[id='{question2}-{question2AnswerIndex}']");
        question2AnswerElement.Change(new ChangeEventArgs());

        var surveyResponse = new SurveyResponse(_memberId, new List<QuestionResponse>()
        {
            new(question1, question1Answer),
            new(question2, question2Answer)
        });
        var answerSurveyDto = new AnswerSurveyDto(_memberId, _surveyId, surveyResponse);

        survey.Submit();
        _surveyService.Verify(service => service.SubmitSurveyResponse(answerSurveyDto));

        var thankYouTitle = appComponent.FindElementByCssSelectorAndTextContent("h1", "Thank you for your reply!");
        Assert.NotNull(thankYouTitle);
    }

    [Fact]
    public void ShouldShowUnableToSubmitSurveyResponseWhenServiceThrowsError()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);

        var questions = new List<Question>() { firstQuestion, secondQuestion };

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var survey = appComponent.Find("form[id='survey']");

        var question1 = 1;
        var question2 = 2;
        var question1AnswerIndex = 1;
        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][question1AnswerIndex];
        var question2AnswerIndex = 1;
        var question2Answer = secondQuestion.Responses[Languages.ENGLISH][question2AnswerIndex];

        var question1AnswerElement = appComponent.Find($"input[id='{question1}-{question1AnswerIndex}']");
        question1AnswerElement.Change(new ChangeEventArgs());
        var question2AnswerElement = appComponent.Find($"input[id='{question2}-{question2AnswerIndex}']");
        question2AnswerElement.Change(new ChangeEventArgs());

        var surveyResponse = new SurveyResponse(_memberId, new List<QuestionResponse>()
        {
            new(question1, question1Answer),
            new(question2, question2Answer)
        });
        var answerSurveyDto = new AnswerSurveyDto(_memberId, _surveyId, surveyResponse);
        _surveyService.Setup(service => service.SubmitSurveyResponse(answerSurveyDto)).ThrowsAsync(new Exception());

        survey.Submit();

        var unableToSubmitSurveyResponseElement =
            appComponent.FindElementByCssSelectorAndTextContent("h1", "Unable to submit survey response");
        Assert.NotNull(unableToSubmitSurveyResponseElement);

        var contactCoachElement = appComponent.FindElementByCssSelectorAndTextContent("p", "Please contact your coach");
        Assert.NotNull(contactCoachElement);
    }


    [Fact]
    public async Task ShouldSendAnswerSurveyDtoToSurveyServiceAndShowThankYouMessageInSpanishOnSubmit()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);
        
        var questions = new List<Question>() { firstQuestion, secondQuestion };

        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var survey = appComponent.Find("form[id='survey']");

        var selectLanguageOption = appComponent.Find("select");
        selectLanguageOption.Change(new ChangeEventArgs(){Value = Languages.SPANISH});
            
        var question1 = 1;
        var question2 = 2;
        var question1AnswerIndex = 1;
        var question1Answer = firstQuestion.Responses[Languages.SPANISH][question1AnswerIndex];
        var question2AnswerIndex = 1;
        var question2Answer = secondQuestion.Responses[Languages.SPANISH][question2AnswerIndex];

        var question1AnswerElement = appComponent.Find($"input[id='{question1}-{question1AnswerIndex}']");
        question1AnswerElement.Change(new ChangeEventArgs());
        var question2AnswerElement = appComponent.Find($"input[id='{question2}-{question2AnswerIndex}']");
        question2AnswerElement.Change(new ChangeEventArgs());

        var surveyResponse = new SurveyResponse(_memberId, new List<QuestionResponse>()
        {
            new(question1, question1Answer),
            new(question2, question2Answer)
        });
        var answerSurveyDto = new AnswerSurveyDto(_memberId, _surveyId, surveyResponse);

        survey.Submit();
        _surveyService.Verify(service => service.SubmitSurveyResponse(answerSurveyDto));

        var thankYouTitleInSpanish = appComponent.FindElementByCssSelectorAndTextContent("h1", "¡Gracias por tu respuesta!");
        Assert.NotNull(thankYouTitleInSpanish);
    }

    [Fact]
    public void ShouldCallCachedResponseServiceWhenLoadingSurveyQuestions()
    {
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        
        _ctx.RenderComponent<SurveyQuestions>(
            ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
        );
        
        _cachedResponseService.Verify(service => service.GetBy(_surveyNotificationId), Times.Once);
    }
    
    [Fact]
    public void ShouldAssignResponsesFromCachedResponsesWhenLoadingSurveyQuestions()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);
        
        var questions = new List<Question>() { firstQuestion, secondQuestion };
        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][1];

        var cachedResponses = new Dictionary<int, string>(){{1, question1Answer}};
        
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(cachedResponses);
        
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );
        
        var question1Answered = appComponent.Find($"input[value='{question1Answer}']");
        Assert.Equal("checked", question1Answered.Attributes.GetNamedItem("data-testid")?.Value);
    }

    [Fact]
    public void ShouldSaveEachResponseInCache()
    {
        SetupQuestions(out var firstQuestion, out var secondQuestion);
        
        var questions = new List<Question>() { firstQuestion, secondQuestion };
        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][1];
        
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );
        
        var question1Element = appComponent.Find($"input[value='{question1Answer}']");
        Assert.NotNull(question1Element);
        question1Element.Change(new ChangeEventArgs());
        
        var expectedCachedResponses = new Dictionary<int, string>(){{1, question1Answer}};
        
        _cachedResponseService.Verify(service => service.Save(_surveyNotificationId, expectedCachedResponses));
    }

    [Fact]
    public void ShouldEmptyResponseInCacheWhenSubmit()
    {
        SetupQuestions(out var question, out _);
        
        var questions = new List<Question>() { question };
        
        _cachedResponseService.Setup(service => service.GetBy(_surveyNotificationId))
            .ReturnsAsync(new Dictionary<int, string>());
        
        _surveyService.Setup(service => service.GetSurveyNotificationById(_surveyNotificationId))
            .ReturnsAsync(new SurveyNotification(Guid.NewGuid(), _surveyId, _memberId));
        _surveyService.Setup(service => service.GetSurveyQuestionsBySurveyId(_surveyId)).ReturnsAsync(questions);

        var appComponent =
            _ctx.RenderComponent<SurveyQuestions>(
                ComponentParameter.CreateParameter("SurveyNotificationId", _surveyNotificationId)
            );

        var question1Element = FindQuestionResponse(appComponent, question, 1);
        Assert.NotNull(question1Element);
        question1Element.Change(new ChangeEventArgs());
        
        var survey = appComponent.Find("form[id='survey']");
        survey.Submit();

        _cachedResponseService.Verify(service => service.Clear(_surveyNotificationId));
    }
    
    private IElement FindQuestionResponse(IRenderedComponent<SurveyQuestions> appComponent, Question question, int responseIndex)
    {
        return appComponent.Find($"input[id='{question.Position + "-" + responseIndex}']");
    }

    private void SetupQuestions(out Question firstQuestion, out Question secondQuestion)
    {
        var responseSpanish1 = "SPA_1";
        var responseSpanish2 = "SPA_2";
        var responseSpanish3 = "SPA_3";
        var responseEnglish1 = "ENG_1";
        var responseEnglish2 = "ENG_2";
        var responseEnglish3 = "ENG_3";
        var reverse = false;

        firstQuestion = new Question(new Dictionary<string, string>()
            {
                { Languages.SPANISH, "Primera pregunta en espanol" },
                { Languages.ENGLISH, "First Question in english" },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { responseSpanish1, responseSpanish2, responseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { responseEnglish1, responseEnglish2, responseEnglish3 }
                }
            }, reverse,
            HackmanSubComponent.InterpersonalPeerCoaching,
            HackmanSubcategory.Delimited, HackmanComponent.SenseOfUrgency, 1);

        secondQuestion = new Question(new Dictionary<string, string>()
            {
                { Languages.SPANISH, "Segunda pregunta en espanol" },
                { Languages.ENGLISH, "Second Question in english" },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { responseSpanish1, responseSpanish2, responseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { responseEnglish1, responseEnglish2, responseEnglish3 }
                }
            }, reverse,
            HackmanSubComponent.InterpersonalPeerCoaching,
            HackmanSubcategory.Delimited, HackmanComponent.SenseOfUrgency, 2);
    }
    
}