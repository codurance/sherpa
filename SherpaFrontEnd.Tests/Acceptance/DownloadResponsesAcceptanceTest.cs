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

public class DownloadResponsesAcceptanceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly FakeNavigationManager _navManager;
    private readonly SurveyService _surveyService;
    private readonly TeamServiceHttpClient _teamDataService;

    public DownloadResponsesAcceptanceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new[] { new Claim("username", "Demo user") });
        _surveyService = new SurveyService(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _teamDataService = new TeamServiceHttpClient(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamDataService);

        _navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task UserShouldBeAbleToClickOnDownloadResponsesButton()
    {
        var teamId = Guid.NewGuid();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{teamId}");
        var team = new Team();
        var teamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}", teamResponse);

        var surveys = new List<Survey>()
        {
            new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Lucia"), Status.Draft, new DateTime(), "title",
                "Description", Array.Empty<Response>(), null, new Template("Hackman"))
        };
        var surveyJson = await JsonContent.Create(surveys).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}/surveys", surveyResponse);
        var appComponent = _testCtx.RenderComponent<App>();
        var teamContentPage = $"/team-content/{teamId}";
        _navManager.NavigateTo(teamContentPage);

        appComponent.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", _navManager.Uri));
        var surveyTab = appComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys");
        _testOutputHelper.WriteLine(appComponent.Markup);
        surveyTab.Click();

        //var icon = appComponent.Find("button:has(i)");
        //icon.Click();

        var downloadButton =
            appComponent.FindElementByCssSelectorAndTextContent("button", "Download responses in .xlsx");
        downloadButton.Click();
        
        
    }
}