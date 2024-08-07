﻿using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Acceptance;

public class SurveysAcceptanceTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<IGuidService> _guidService;
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly FakeNavigationManager _navMan;
    private readonly TeamServiceHttpClient _teamsService;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly SurveyService _surveyService;
    private Mock<IAuthService> _authService;

    public SurveysAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddBlazoredToast();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _guidService = new Mock<IGuidService>();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);
        _surveyService = new SurveyService(_factoryHttpClient.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _testCtx.Services.AddScoped<IToastNotificationService, BlazoredToastService>();
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new []{new Claim("username", "Demo user")});
        _testCtx.Services.AddSingleton<IAnalysisService>(new Mock<IAnalysisService>().Object);
        _testCtx.JSInterop.SetupVoid("generateColumnsChart", _ => true);
        
        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }
    
    [Fact]
    public async Task ShouldBeAbleToViewEmptySurveyPageFromSurveyTabWhenUserHasNoSurveys()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);
        
        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(newTeamId);

        var emptyTeamsList = new List<Team>(){};
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var emptyTeamsListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "/team", emptyTeamsListResponse);
        
        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post, "", creationResponse);
        
        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}", singleTeamResponse);

        var emptySurveyList = new List<Survey>() {};
        var emptySurveyListJson = await JsonContent.Create(emptySurveyList).ReadAsStringAsync();
        var emptySurveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptySurveyListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}/surveys", emptySurveyListResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        
        var teamsListPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{teamsListPage}']");
        Assert.NotNull(teamsPageLink);

        _navMan.NavigateTo($"/{teamsListPage}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/teams-list-page", _navMan.Uri));

        var createNewTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();
        
        var teamNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change(newTeamName);
        
        var confirmButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();
        
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{newTeamId.ToString()}", _navMan.Uri));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("h1", newTeamName));

        GoToTab(appComponent, "Surveys");

        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("button", "Launch first survey")));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "You don’t have any surveys yet"));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Let's begin the journey towards a stronger, more effective team!"));
    }

    [Fact]
    public async Task ShouldBeAbleToViewEmptySurveyPageFromAnalysisTabWhenUserHasNoSurveys()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);
        
        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(newTeamId);

        var emptyTeamsList = new List<Team>(){};
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var emptyTeamsListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "/team", emptyTeamsListResponse);
        
        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post, "", creationResponse);
        
        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}", singleTeamResponse);

        var emptySurveyList = new List<Survey>() {};
        var emptySurveyListJson = await JsonContent.Create(emptySurveyList).ReadAsStringAsync();
        var emptySurveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptySurveyListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{newTeamId.ToString()}/surveys", emptySurveyListResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        
        var teamsListPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{teamsListPage}']");
        Assert.NotNull(teamsPageLink);

        _navMan.NavigateTo($"/{teamsListPage}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/teams-list-page", _navMan.Uri));

        var createNewTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();
        
        var teamNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change(newTeamName);
        
        var confirmButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();
        
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{newTeamId.ToString()}", _navMan.Uri));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("h1", newTeamName));

        var teamSurveysTabPage = appComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Analysis");
        Assert.NotNull(teamSurveysTabPage);
        
        teamSurveysTabPage.Click();
        
        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("button", "Launch first survey")));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Here you will see the analysis reports from the surveys"));
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Let's begin the journey towards a stronger, more effective team!"));
    }

    [Fact]
    public async Task ShouldBeAbleToViewSurveyPageFromSurveyTabWhenUserHasSurveys()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);
        
        await SetupSurveyForTeam(newTeam);
        
        var appComponent = _testCtx.RenderComponent<App>();
        
        NavigateToTeamPage(appComponent, newTeam);

        GoToSurveysTab(appComponent);
        
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("button", "Launch new survey"));
    }
    
    [Fact]
    public async Task ShouldBeAbleToViewParticipationRateInSurveyPageFromSurveyTabWhenUserHasSurveys()
    {
        const string newTeamName = "Team with surveys";
        var newTeamId = Guid.NewGuid();
        var newTeam = new Team(newTeamId, newTeamName);

        var survey = await SetupSurveyForTeam(newTeam);
        
        var appComponent = _testCtx.RenderComponent<App>();
        
        NavigateToTeamPage(appComponent, newTeam);

        GoToSurveysTab(appComponent);

        var participantsElement = appComponent.FindElementByCssSelectorAndTextContent("div",$"{survey.Responses.Length} / {survey.Team.Members.Count}");
        Assert.NotNull(participantsElement);
    }
    

    private async Task<Survey> SetupSurveyForTeam(Team team)
    {
        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(team.Id);

        var emptyTeamsList = new List<Team>(){};
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var emptyTeamsListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "/team",emptyTeamsListResponse );
        
        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post,"",creationResponse);
        
        var singleTeamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{team.Id.ToString()}", singleTeamResponse);
        
        var userOne = new User(Guid.NewGuid(), "user");
        var survey = new Survey(Guid.NewGuid(), userOne, Status.InProgress, new DateTime(), "title", "description",
            new []{new Response()}, team, new Template("template"));
        var surveyList = new List<Survey>(){ survey };
        var surveyListJson = await JsonContent.Create(surveyList).ReadAsStringAsync();
        var surveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{team.Id.ToString()}/surveys",surveyListResponse );
        return survey;
    }
    
    private void NavigateToTeamPage(IRenderedComponent<App> appComponent, Team team)
    {
        var teamsListPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{teamsListPage}']");
        Assert.NotNull(teamsPageLink);

        _navMan.NavigateTo($"/{teamsListPage}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/teams-list-page", _navMan.Uri));

        var createNewTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();
        
        var teamNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change(team.Name);
        
        var confirmButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{team.Id.ToString()}", _navMan.Uri));
    }
    
    private static void GoToSurveysTab(IRenderedComponent<App> appComponent)
    {
        var teamSurveysTabPage = appComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys");
        Assert.NotNull(teamSurveysTabPage);
        
        teamSurveysTabPage.Click();
        
        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("h2", "All surveys launched in the team")));
    }
    
    private static void GoToTab(IRenderedComponent<App> appComponent, string tabName)
    {
        var teamSurveysTabPage = appComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", tabName);
        Assert.NotNull(teamSurveysTabPage);
        teamSurveysTabPage.Click();
    }
}