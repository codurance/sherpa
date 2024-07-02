using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Blazored.LocalStorage;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class DownloadResponsesAcceptanceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly Mock<IAuthService> _authService;
    private readonly FakeNavigationManager _navManager;
    private readonly SurveyService _surveyService;
    private readonly TeamServiceHttpClient _teamDataService;

    public DownloadResponsesAcceptanceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new[] { new Claim("username", "Demo user") });
        _surveyService = new SurveyService(_httpClientFactory.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _teamDataService = new TeamServiceHttpClient(_httpClientFactory.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamDataService);

        _navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task UserShouldBeAbleToClickOnDownloadResponsesButtonAndSendDownloadRequest()
    {
        var teamId = Guid.NewGuid();
        var team = new Team();
        var teamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}", teamResponse);

        var surveyId = Guid.NewGuid();
        var surveyTitle = "title";
        var surveys = new List<Survey>()
        {
            new Survey(surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), surveyTitle,
                "Description", Array.Empty<Response>(), null, new Template("Hackman"))
        };
        var surveyJson = await JsonContent.Create(surveys).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}/surveys", surveyResponse);

        var downloadSurveyPath = $"survey/{surveyId}/responses";
        var csvText = "Responses,1,2,3";
        var fileBytes = Encoding.UTF8.GetBytes(csvText);
        var downloadSurveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new ByteArrayContent(fileBytes)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, downloadSurveyPath, downloadSurveyResponse);

        // Given an Org Coach is on the survey tab inside a team
        var appComponent = _testCtx.RenderComponent<App>();
        var teamContentPageSurveys = $"/team-content/{teamId}/surveys";
        _navManager.NavigateTo(teamContentPageSurveys);

        appComponent.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/team-content/{teamId.ToString()}/surveys", _navManager.Uri));

        // When they click on the 3 dots next to a survey (this can't be clicked from the test)
        var downloadButton =
            appComponent.FindElementByCssSelectorAndTextContent("button", "Download all responses");

        // Then they should see the button "Download responses in .xlsx"
        Assert.NotNull(downloadButton);

        // And when they click on the button
        downloadButton.Click();

        //TODO: check if more is needed to download
        // Then a report should be downloaded
        _testCtx.JSInterop.VerifyInvoke("downloadFile", 1);

        _handlerMock.Protected().Verify("SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(message =>
                message.Method.Equals(HttpMethod.Get) &&
                message.RequestUri!.AbsoluteUri.Contains(downloadSurveyPath)),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task UserShouldBeRedirectedToErrorPageIfDownloadFails()
    {
        var teamId = Guid.NewGuid();
        var team = new Team();
        var teamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}", teamResponse);

        var surveyId = Guid.NewGuid();
        var surveys = new List<Survey>()
        {
            new Survey(surveyId, new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), "title",
                "Description", Array.Empty<Response>(), null, new Template("Hackman"))
        };
        var surveyJson = await JsonContent.Create(surveys).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}/surveys", surveyResponse);

        var downloadSurveyPath = $"survey/{surveyId}/responses";
        var downloadSurveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.InternalServerError,
        };
        _handlerMock.SetupRequest(HttpMethod.Get, downloadSurveyPath, downloadSurveyResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        var teamContentPageSurveys = $"/team-content/{teamId}/surveys";
        _navManager.NavigateTo(teamContentPageSurveys);

        appComponent.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/team-content/{teamId.ToString()}/surveys", _navManager.Uri));

        var downloadButton =
            appComponent.FindElementByCssSelectorAndTextContent("button", "Download all responses");

        downloadButton.Click();

        appComponent.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navManager.Uri));
    }
}