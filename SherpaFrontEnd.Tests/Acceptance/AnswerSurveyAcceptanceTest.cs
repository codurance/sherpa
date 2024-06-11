using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaBackEnd.Survey.Domain.Exceptions;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class AnswerSurveyAcceptanceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly FakeNavigationManager _navManager;
    private readonly Guid _surveyId = Guid.NewGuid();
    private readonly Mock<IGuidService> _guidService;
    private readonly SurveyService _surveyService;

    public AnswerSurveyAcceptanceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _surveyService = new SurveyService(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(_surveyId);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new[] { new Claim("username", "Demo user") });
        _navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task UserShouldBeAbleToNavigateToAnswerSurveyPage()
    {
        var teamMemberId = Guid.NewGuid();
        var surveyTitle = "Title";
        var survey = new Survey(_surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), surveyTitle,
            "Description", Array.Empty<Response>(), null, new Template("Hackman"));
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(surveyJson) };
        var questions = new List<Question>();
        var questionsJson = await JsonContent.Create(questions).ReadAsStringAsync();
        var questionsResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(questionsJson) };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}", surveyResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}/questions", questionsResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        var answerSurveyPage = $"/answer-survey/{_surveyId}/{teamMemberId}";
        _navManager.NavigateTo(answerSurveyPage);

        var surveyTitleElement = appComponent.FindElementByCssSelectorAndTextContent("h2", surveyTitle);

        Assert.NotNull(surveyTitleElement);
    }


    [Theory]
    [InlineData("English")]
    [InlineData("Spanish")]
    public async Task UserShouldBeAbleToSeeQuestionsInSelectedLanguage(string selectedLanguage)
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

        var teamMemberId = Guid.NewGuid();
        var surveyTitle = "Title";
        var survey = new Survey(_surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), surveyTitle,
            "Description", Array.Empty<Response>(), null, new Template("Hackman"));
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(surveyJson) };
        var questions = new List<Question>() { question };
        var questionsJson = await JsonContent.Create(questions).ReadAsStringAsync();
        var questionsResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(questionsJson) };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}", surveyResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}/questions", questionsResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        var answerSurveyPage = $"/answer-survey/{_surveyId}/{teamMemberId}";
        _navManager.NavigateTo(answerSurveyPage);

        var languageSelectElement = appComponent.Find("select");
        languageSelectElement.Change(selectedLanguage.ToUpper());

        var surveyQuestionElement =
            appComponent.FindElementByCssSelectorAndTextContent("legend",
                question.Statement[selectedLanguage.ToUpper()]);
        Assert.NotNull(surveyQuestionElement);

        var surveyResponseLabel1 =
            appComponent.FindElementByCssSelectorAndTextContent("label",
                question.Responses[selectedLanguage.ToUpper()][0]);
        var surveyResponseLabel2 =
            appComponent.FindElementByCssSelectorAndTextContent("label",
                question.Responses[selectedLanguage.ToUpper()][1]);
        var surveyResponseLabel3 =
            appComponent.FindElementByCssSelectorAndTextContent("label",
                question.Responses[selectedLanguage.ToUpper()][2]);

        var surveyResponseInput1 =
            appComponent.Find($"input[id='{surveyResponseLabel1.Attributes.GetNamedItem("for").Value}']");
        Assert.NotNull(surveyResponseInput1);
        var surveyResponseInput2 =
            appComponent.Find($"input[id='{surveyResponseLabel2.Attributes.GetNamedItem("for").Value}']");
        Assert.NotNull(surveyResponseInput2);
        var surveyResponseInput3 =
            appComponent.Find($"input[id='{surveyResponseLabel3.Attributes.GetNamedItem("for").Value}']");
        Assert.NotNull(surveyResponseInput3);
    }


    [Fact]
    public async Task UserShouldSeeThankYouForReplyingWhenSuccessfulySubmittingForm()
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

        var teamMemberId = Guid.NewGuid();
        var surveyTitle = "Title";
        var survey = new Survey(_surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), surveyTitle,
            "Description", Array.Empty<Response>(), null, new Template("Hackman"));
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(surveyJson) };
        var questionsJson = await JsonContent.Create(questions).ReadAsStringAsync();
        var questionsResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(questionsJson) };
        var submitAnswersResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}", surveyResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}/questions", questionsResponse);
        _handlerMock.SetupRequest(HttpMethod.Post, $"/survey/{_surveyId}/team-members/{teamMemberId}",
            submitAnswersResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        var answerSurveyPage = $"/answer-survey/{_surveyId}/{teamMemberId}";
        _navManager.NavigateTo(answerSurveyPage);

        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][1];
        var question2Answer = secondQuestion.Responses[Languages.ENGLISH][1];

        var question1AnswerElement = appComponent.Find($"input[value='{question1Answer}']");
        question1AnswerElement.Change(new ChangeEventArgs());
        var question2AnswerElement = appComponent.Find($"input[value='{question2Answer}']");
        question2AnswerElement.Change(new ChangeEventArgs());

        var surveyElement = appComponent.Find("form[id='survey']");
        surveyElement.Submit();

        var thankYouTitle = appComponent.FindElementByCssSelectorAndTextContent("h1", "Thank you for your reply!");
        Assert.NotNull(thankYouTitle);
    }

    [Fact]
    public async Task UserShouldSeeAContactYourCoachPageWhenSubmittingAFormFails()
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

        var teamMemberId = Guid.NewGuid();
        var surveyTitle = "Title";
        var survey = new Survey(_surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), surveyTitle,
            "Description", Array.Empty<Response>(), null, new Template("Hackman"));
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(surveyJson) };
        var questionsJson = await JsonContent.Create(questions).ReadAsStringAsync();
        var questionsResponse = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.OK, Content = new StringContent(questionsJson) };
        var submitAnswersResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.InternalServerError
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}", surveyResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{_surveyId}/questions", questionsResponse);
        _handlerMock.SetupRequest(HttpMethod.Post, $"/survey/{_surveyId}/team-members/{teamMemberId}",
            submitAnswersResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        var answerSurveyPage = $"/answer-survey/{_surveyId}/{teamMemberId}";
        _navManager.NavigateTo(answerSurveyPage);

        var question1Answer = firstQuestion.Responses[Languages.ENGLISH][1];
        var question2Answer = secondQuestion.Responses[Languages.ENGLISH][1];

        var question1AnswerElement = appComponent.Find($"input[value='{question1Answer}']");
        question1AnswerElement.Change(new ChangeEventArgs());
        var question2AnswerElement = appComponent.Find($"input[value='{question2Answer}']");
        question2AnswerElement.Change(new ChangeEventArgs());

        var surveyElement = appComponent.Find("form[id='survey']");
        surveyElement.Submit();

        var unableToSubmitSurveyResponseElement =
            appComponent.FindElementByCssSelectorAndTextContent("h1", "Unable to submit survey response");
        Assert.NotNull(unableToSubmitSurveyResponseElement);

        var contactCoachElement = appComponent.FindElementByCssSelectorAndTextContent("p", "Please contact your coach");
        Assert.NotNull(contactCoachElement);
    }
}