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
    private Mock<IHttpClientFactory>  _factoryHttpClient;

    public AnalysisAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddBlazoredToast();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new []{new Claim("username", "Demo user")});
        _navMan =  _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task ShouldBeAbleToViewGeneralResultsGraph()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);
        
        var appComponent = _testCtx.RenderComponent<App>();
        
        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}", singleTeamResponse);

        var userOne = new User(Guid.NewGuid(), "user");
        var survey = new Survey(Guid.NewGuid(), userOne, Status.InProgress, new DateTime(), "title", "description",
            new []{new Response()}, newTeam, new Template("template"));
        var surveyList = new List<Survey>(){ survey };
        var surveyListJson = await JsonContent.Create(surveyList).ReadAsStringAsync();
        var surveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeam.Id.ToString()}/surveys",surveyListResponse );

        
        _navMan.NavigateTo($"team-content/{newTeam.Id}");
        
        var categories = new string[]
        {
            "Real team",
            "Compelling direction",
            "Expert coaching",
            "Enable structure",
            "Supportive org coaching"
        };
        var maxValue = 1.0;
        
        var firstSurvey = new ColumnSeries<double>("Survey 1", new List<double>(){ 0.5,
            0.5,
            0.5,
            0.5,
            0.5});
        var secondSurvey = new ColumnSeries<double>("Survey 2", new List<double>(){ 0.5,
            0.5,
            0.5,
            0.5,
            0.5});
        var series = new List<ColumnSeries<double>>(){firstSurvey, secondSurvey};
        var columnChart = new ColumnChart<double>(categories, series, maxValue);
        
        var generalResults = new GeneralResultsDto(columnChart);

        var generalResultsJson = await JsonContent.Create(generalResults).ReadAsStringAsync();
        var generalResultsResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(generalResultsJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"team/{newTeam.Id}/analysis/general-results", generalResultsResponse);
        
        var jsRuntimeInvocation = _testCtx.JSInterop.Invocations.ToList().Find(invocation => invocation.Identifier.Equals("generateColumnsChart"));
        Assert.NotNull(jsRuntimeInvocation.Identifier);
        Assert.Contains(series, jsRuntimeInvocation.Arguments);
        Assert.Contains(categories, jsRuntimeInvocation.Arguments);
        Assert.Contains(maxValue, jsRuntimeInvocation.Arguments);
        var firstCategory = appComponent.FindElementByCssSelectorAndTextContent("text",categories[0]);
        Assert.NotNull(firstCategory);
    }
}