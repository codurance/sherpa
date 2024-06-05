using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
        auth.SetClaims(new []{new Claim("username", "Demo user")});
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
}