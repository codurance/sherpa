using System.Net;
using System.Net.Http.Json;
using Bunit;
using Moq;
using Moq.Protected;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Services;
using Newtonsoft.Json;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using HackmanComponent = SherpaFrontEnd.Dtos.Survey.HackmanComponent;
using HackmanSubcategory = SherpaFrontEnd.Dtos.Survey.HackmanSubcategory;
using HackmanSubcomponent = SherpaFrontEnd.Dtos.Survey.HackmanSubcomponent;
using IQuestion = SherpaFrontEnd.Dtos.Survey.IQuestion;
using Languages = SherpaFrontEnd.Dtos.Survey.Languages;
using Template = SherpaFrontEnd.Dtos.Survey.Template;

namespace BlazorApp.Tests.Services;

public class SurveyServiceTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly Team[] _teams;

    public SurveyServiceTest()
    {
        _testCtx = new TestContext();
        _teams = new[] { new Team(Guid.NewGuid(), "Demo Team") };

        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
    }

    [Fact]
    public async Task ShouldReturnSurveyInHttpResponse()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var surveyId = Guid.NewGuid();
        var survey = new SurveyWithoutQuestions(
            surveyId,
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            "Description",
            Array.Empty<Response>(),
            _teams[0],
            templateWithoutQuestions);

        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{surveyId.ToString()}", surveyResponse);

        var surveyService = new SurveyService(_httpClientFactory.Object);
        var actualSurvey = await surveyService.GetSurveyWithoutQuestionsById(surveyId);
        CustomAssertions.StringifyEquals(survey, actualSurvey);
    }

    [Fact]
    public async Task ShouldSendCreateSurveyDtoByHttp()
    {
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var surveyId = Guid.NewGuid();
        var createSurveyDto = new CreateSurveyDto(
            surveyId, _teams[0].Id, templateWithoutQuestions.Name, "Title", "Description", deadline);


        var createSurveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        _handlerMock.SetupRequest(HttpMethod.Post, "/survey", createSurveyResponse);

        var surveyService = new SurveyService(_httpClientFactory.Object);
        await surveyService.CreateSurvey(createSurveyDto);

        _handlerMock.Protected().Verify("SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(
                m => m.Method.Equals(HttpMethod.Post) && m.RequestUri!.AbsoluteUri.Contains("/survey")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnEmptyListOfSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveys = new List<Survey>() { };
        var surveysJson = await JsonContent.Create(surveys).ReadAsStringAsync();

        var emptySurveyListResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveysJson),
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}/surveys", emptySurveyListResponse);


        var surveyService = new SurveyService(_httpClientFactory.Object);

        var actualResponse = await surveyService.GetAllSurveysByTeam(teamId);

        var expectedResponse = new List<Survey> { };
        Assert.Equal(JsonConvert.SerializeObject(expectedResponse), JsonConvert.SerializeObject(actualResponse));
    }

    [Fact]
    public async Task ShouldReturnExpectedListOfSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var userOne = new User(Guid.NewGuid(), "user");
        var team = new Team(Guid.NewGuid(), "name");
        var surveys = new List<Survey>()
        {
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
                Array.Empty<Response>(), team, new Template("template"))
        };
        var surveysJson = await JsonContent.Create(surveys).ReadAsStringAsync();

        var responseWithSurveys = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveysJson),
        };

        _handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri!.AbsoluteUri.Contains($"team/{teamId.ToString()}/surveys")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithSurveys);


        var surveyService = new SurveyService(_httpClientFactory.Object);

        var actualResponse = await surveyService.GetAllSurveysByTeam(teamId);

        Assert.Equal(JsonConvert.SerializeObject(surveys), JsonConvert.SerializeObject(actualResponse));
    }

    [Fact]
    public async Task ShouldReturnSurveyQuestionsInHttpResponse()
    {
        var surveyId = Guid.NewGuid();
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

        var questions = new List<IQuestion>() { question };
        var questionsListJson = await JsonContent.Create(questions).ReadAsStringAsync();
        var questionsListResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(questionsListJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"/survey/{surveyId.ToString()}/questions", questionsListResponse);

        var surveyService = new SurveyService(_httpClientFactory.Object);
        
        var surveyQuestions = await surveyService.GetSurveyQuestionsBySurveyId(surveyId);
        
        Assert.Equal(JsonConvert.SerializeObject(questions), JsonConvert.SerializeObject(surveyQuestions));
    }
}