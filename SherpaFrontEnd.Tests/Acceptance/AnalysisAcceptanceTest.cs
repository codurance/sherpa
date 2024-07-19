using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using AngleSharp.Common;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Analysis;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Acceptance;

public class AnalysisAcceptanceTest
{
    private TestContext _testCtx;
    private FakeNavigationManager _navMan;
    private Mock<HttpMessageHandler> _httpHandlerMock;
    private Mock<IHttpClientFactory> _factoryHttpClient;
    private Mock<IAuthService> _authService;

    public AnalysisAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddBlazoredToast();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.Services.AddScoped<IAnalysisService, AnalysisService>();
        _testCtx.Services.AddScoped<IToastNotificationService, BlazoredToastService>();
        _testCtx.Services.AddScoped<ISurveyService, SurveyService>();
        _testCtx.Services.AddScoped<ITeamDataService, TeamServiceHttpClient>();
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _testCtx.Services.AddSingleton<IAuthService>(_authService.Object);
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _testCtx.Services.AddSingleton<IHttpClientFactory>(_factoryHttpClient.Object);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new[] { new Claim("username", "Demo user") });
        _testCtx.JSInterop.SetupVoid("generateColumnsChart", _ => true);
        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task ShouldBeAbleToViewGeneralResultsGraph()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);


        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}", singleTeamResponse);

        var userOne = new User(Guid.NewGuid(), "user");
        var survey = new Survey(Guid.NewGuid(), userOne, Status.InProgress, new DateTime(), "title", "description",
            new[] { new Response() }, newTeam, new Template("template"));
        var surveyList = new List<Survey>() { survey };
        var surveyListJson = await JsonContent.Create(surveyList).ReadAsStringAsync();
        var surveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyListJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeam.Id.ToString()}/surveys", surveyListResponse);

        var generalResults = AnalysisHelper.BuildGeneralResultsDto();

        var generalResultsJson = await JsonContent.Create(generalResults).ReadAsStringAsync();
        var generalResultsResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(generalResultsJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"team/{newTeam.Id}/analysis/general-results",
            generalResultsResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo($"team-content/{newTeam.Id}");

        appComponent.WaitForAssertion(() =>
        {
            Assert.Equal($"http://localhost/team-content/{newTeam.Id}", _navMan.Uri);
        });
        var generalResultsTitle = appComponent.FindElementByCssSelectorAndTextContent("h2", "General results");
        Assert.NotNull(generalResultsTitle);
        var jsRuntimeInvocation = _testCtx.JSInterop.Invocations.ToList()
            .Find(invocation => invocation.Identifier.Equals("generateColumnsChart"));
        Assert.NotNull(jsRuntimeInvocation.Identifier);
        CustomAssertions.StringifyEquals(generalResults.ColumnChart, jsRuntimeInvocation.Arguments[1]);
        CustomAssertions.StringifyEquals(generalResults.Metrics.General, jsRuntimeInvocation.Arguments[2]);
    }
    
    [Fact]
    public async Task ShouldBeAbleToViewGeneralResultsMetrics()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);


        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}", singleTeamResponse);

        var userOne = new User(Guid.NewGuid(), "user");
        var survey = new Survey(Guid.NewGuid(), userOne, Status.InProgress, new DateTime(), "title", "description",
            new[] { new Response() }, newTeam, new Template("template"));
        var surveyList = new List<Survey>() { survey };
        var surveyListJson = await JsonContent.Create(surveyList).ReadAsStringAsync();
        var surveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyListJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeam.Id.ToString()}/surveys", surveyListResponse);

        var generalResults = AnalysisHelper.BuildGeneralResultsDto();

        var generalResultsJson = await JsonContent.Create(generalResults).ReadAsStringAsync();
        var generalResultsResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(generalResultsJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"team/{newTeam.Id}/analysis/general-results",
            generalResultsResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo($"team-content/{newTeam.Id}");

        appComponent.WaitForAssertion(() =>
        {
            Assert.Equal($"http://localhost/team-content/{newTeam.Id}", _navMan.Uri);
        });


        AssertSurveyMetricsSection(appComponent);

        var informativeMessage = appComponent.FindElementByCssSelectorAndTextContent("p",
            "The following numbers are a comparison between the last and previous survey assessment");
        Assert.NotNull(informativeMessage);

        AssertCategoriesMetricsSection(appComponent);

        var arrowUp = appComponent.Find(".text-states-success-800");
        Assert.NotNull(arrowUp);
        Assert.Equal(2, appComponent.FindAll(".text-states-success-800").Count);

        var arrowDown = appComponent.Find(".text-states-error-800");
        Assert.NotNull(arrowDown);
        Assert.Equal(3, appComponent.FindAll(".text-states-error-800").Count);
    }

    private static void AssertCategoriesMetricsSection(IRenderedComponent<App> appComponent)
    {
        var firstSurveyCategoryName = appComponent.FindElementByCssSelectorAndTextContent("p", "Real team");
        Assert.NotNull(firstSurveyCategoryName);
        var firstSurveyCategoryAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "74%");
        Assert.NotNull(firstSurveyCategoryAverage);
        
        var secondSurveyCategoryName = appComponent.FindElementByCssSelectorAndTextContent("p", "Enable Structure");
        Assert.NotNull(secondSurveyCategoryName);
        var secondSurveyCategoryAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "68%");
        Assert.NotNull(secondSurveyCategoryAverage);
        
        var thirdSurveyCategoryName = appComponent.FindElementByCssSelectorAndTextContent("p", "Expert coaching");
        Assert.NotNull(thirdSurveyCategoryName);
        var thirdSurveyCategoryAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "81%");
        Assert.NotNull(thirdSurveyCategoryAverage);
        
        var fourthSurveyCategoryName = appComponent.FindElementByCssSelectorAndTextContent("p", "Supportive org coaching");
        Assert.NotNull(fourthSurveyCategoryName);
        var fourthSurveyCategoryAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "88%");
        Assert.NotNull(fourthSurveyCategoryAverage);
        
        var fifthSurveyCategoryName = appComponent.FindElementByCssSelectorAndTextContent("p", "Compelling direction");
        Assert.NotNull(fifthSurveyCategoryName);
        var fifthSurveyCategoryAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "64%");
        Assert.NotNull(fifthSurveyCategoryAverage);
    }

    private void AssertSurveyMetricsSection(IRenderedComponent<App> appComponent)
    {
        var firstSurveyMetricName = appComponent.FindElementByCssSelectorAndTextContent("p", "[SURVEY 1]");
        Assert.NotNull(firstSurveyMetricName);
        var firstSurveyMetricAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "50%");
        Assert.NotNull(firstSurveyMetricAverage);
        var secondSurveyMetricName = appComponent.FindElementByCssSelectorAndTextContent("p", "[SURVEY 2]");
        Assert.NotNull(secondSurveyMetricName);
        var secondSurveyMetricAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "66%");
        Assert.NotNull(secondSurveyMetricAverage);
        var thirdSurveyMetricName = appComponent.FindElementByCssSelectorAndTextContent("p", "[SURVEY 3]");
        Assert.NotNull(thirdSurveyMetricName);
        var thirdSurveyMetricAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "70%");
        Assert.NotNull(thirdSurveyMetricAverage);
        var fourthSurveyMetricName = appComponent.FindElementByCssSelectorAndTextContent("p", "[SURVEY 4]");
        Assert.NotNull(fourthSurveyMetricName);
        var fourthSurveyMetricAverage = appComponent.FindElementByCssSelectorAndTextContent("p", "83%");
        Assert.NotNull(fourthSurveyMetricAverage);
    }
}