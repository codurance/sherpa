using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Pages.TeamList;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class TeamsAcceptanceTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly TeamServiceHttpClient _teamsService;
    private readonly FakeNavigationManager _navMan;
    private readonly Mock<IGuidService> _guidService;
    private ITestOutputHelper _output;
    private readonly ISurveyService _surveyService;

    public TeamsAcceptanceTest(ITestOutputHelper output)
    {
        _output = output;

        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object);
        _surveyService = new SurveyService(_factoryHttpClient.Object);
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new []{new Claim("username", "Demo user")});
        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    private async Task UserShouldBeAbleToNavigateToTeamsPageWithoutTeamsAndSeeItsComponents()
    {
        var emptyTeamsList = new List<Team>() { };
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", responseEmpty);

        var appComponent = _testCtx.RenderComponent<App>();
        
        var targetPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{targetPage}']");
        Assert.NotNull(teamsPageLink);
        var navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
        navManager.NavigateTo($"/{targetPage}");

        var allTeamsTitle = appComponent.FindElementByCssSelectorAndTextContent("h1,h2,h3", "All teams");
        Assert.NotNull(allTeamsTitle);

        var createTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createTeamButton);
    }

    [Fact]
    private async Task UserShouldBeAbleToNavigateToTeamsPageWithTeamsAndSeeItsComponents()
    {
        var teamName = "Team name";
        var teamsList = new List<Team>() { new Team(Guid.NewGuid(), teamName) };
        var reamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(reamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", response);

        var appComponent = _testCtx.RenderComponent<App>();


        var targetPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{targetPage}']");
        Assert.NotNull(teamsPageLink);
        var navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
        navManager.NavigateTo($"/{targetPage}");

        var allTeamsTitle = appComponent.FindElementByCssSelectorAndTextContent("h1,h2,h3", "All teams");
        Assert.NotNull(allTeamsTitle);

        var createTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createTeamButton);

        var teamNameElement =
            appComponent.FindElementByCssSelectorAndTextContent("h2", teamName);
        Assert.NotNull(teamNameElement);
    }

    [Fact]
    private async Task ShouldBeAbleToSeeCreateNewTeamFormWhenClickingOnCreateNewTeamInTeamsPage()
    {
        // GIVEN that an Org coach is on the All Teams pag

        var emptyTeamsList = new List<Team>() { };
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", responseEmpty);

        var teamsListComponent = _testCtx.RenderComponent<TeamsList>();

        // WHEN he clicks on “+ Create a new team“
        var createNewTeamButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();


        // THEN he should be redirected on the page for creating a team
        //     with one mandatory text field “Team´s name”
        //     and 2 buttons Cancel and Confirm
        var createNewTeamTitle = teamsListComponent.FindElementByCssSelectorAndTextContent("h2", "Create new team");
        Assert.NotNull(createNewTeamTitle);
        var teamNameLabel = teamsListComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.FindAll($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        var confirmButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);
        var cancelButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);
    }

    [Fact]
    private async Task ShouldBeAbleToCreateATeamAndBeRedirectedToTeamPage()
    {
        //GIVEN that an Org coach is on the page for creating a team

        var emptyTeamsList = new List<Team>() { };
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", responseEmpty);

        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post, "", creationResponse);

        var teamsListComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        //    WHEN he enters a team name
        //    and clicks on Confirm

        var teamId = Guid.NewGuid();

        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(teamId);

        const string teamName = "Team name";
        var newTeam = new Team(teamId, teamName);
        var newTeamAsJson = await JsonContent.Create(newTeam).ReadAsStringAsync();

        var teamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(newTeamAsJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}", teamResponse);
        
        var teamSurveysResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}/surveys", teamSurveysResponse);

        var teamNameLabel = teamsListComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        var confirmButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();

        //    THEN he should be redirected on this Team page (Analysis tab)
        //and on the Team page he should see the following info:
        //Team name without Editing
        //Tab Analysis
        //the button “Send a new survey“ without functionality
        
        teamsListComponent.WaitForAssertion(()=> Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", _navMan.Uri));

        _navMan.NavigateTo($"/team-content/{teamId.ToString()}");
        
        teamsListComponent.WaitForAssertion(() => Assert.NotNull(teamsListComponent.FindElementByCssSelectorAndTextContent("h1", teamName)));


        var analysisTab = teamsListComponent.FindElementByCssSelectorAndTextContent("li", "Analysis");
        var sendNewSurveyTeam = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Launch new survey");

        Assert.NotNull(analysisTab);
        Assert.NotNull(sendNewSurveyTeam);
    }

    [Fact]
    public async Task ShouldBeAbleToClickCancelInTeamCreationFormAndStayInTheSamePage()
    {
        //GIVEN that an Org coach is on the page for creating a team

        var emptyTeamsList = new List<Team>() { };
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);

        _testCtx.JSInterop.SetupVoid("hideOffCanvas", "create-new-team-form").SetVoidResult();
        var teamsListComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        // WHEN he enters a team name

        var teamId = Guid.NewGuid();

        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(teamId);

        var teamNameLabel = teamsListComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        // and clicks on Cancel

        var cancelButton = teamsListComponent.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);

        cancelButton.Click();

        // THEN he should be redirected on the All Teams page
        // and a team should not be created

        Assert.Equal($"http://localhost/teams-list-page", _navMan.Uri);
    }

    [Fact]
    public async Task ShouldBeAbleToClickOnATeamAndNavigateToItsOwnPage()
    {
        // GIVEN that an Org coach is on the All Teams page

        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamsList = new List<Team>() { team };
        var teamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var teamListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamListJson)
        };

        // and he has already created a team
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", teamListResponse);

        var singleTeamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}", singleTeamResponse);
        
        var teamSurveysResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[]")
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}/surveys", teamSurveysResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        // and he can click on this team

        var existingTeamNameElement =
            appComponent.FindElementByCssSelectorAndTextContent("h2", teamName);
        Assert.NotNull(existingTeamNameElement);

        existingTeamNameElement.Click();

        Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", _navMan.Uri);
    }

    [Fact]
    public async Task ShouldRedirectToErrorPageIfFoundErrorWhenAddingTeam()
    {
        // GIVEN that an Org coach is on the Creating a new team page

        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamsList = new List<Team>() { team };
        var teamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var teamListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", teamListResponse);

        var singleTeamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post, $"/team/{teamId.ToString()}", singleTeamResponse);

        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.InternalServerError,
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Post, "", creationResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        // WHEN he enters Teams name

        var teamNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        //     and click on Confirm

        var confirmButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();

        //     and something went wrong (we could not create a new team)
        // THEN he should see the error message “Something went wrong“ at the top of the page
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/error", _navMan.Uri));


        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Something went wrong."));
    }

    [Fact]
    public async Task ShouldShowFeedbackIfTryingToCreateATeamWithoutFillingTheNameInput()
    {
        // GIVEN that an Org coach is on the Creating a new team page
        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamsList = new List<Team>() { team };
        var teamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var teamListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamListJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", teamListResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Create new team");
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        // WHEN he clicks on Confirm
        // and he didn't enter anything to the mandatory field Teams name

        var confirmButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);

        confirmButton.Click();

        appComponent.WaitForElement(".validation-message");

        // THEN this field should be highlighted in read and at the top of the page he should see an error message that it's mandatory field.

        var teamNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team name");

        var inputGroup = teamNameLabel.Parent;


        Assert.Contains("This field is mandatory", inputGroup.ToMarkup());
    }

    [Fact]
    public async Task ShouldBeRedirectedToErrorPageIfThereIsAnErrorLoadingTeams()
    {
        // GIVEN that an Org coach have a menu on the left
        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamsList = new List<Team>() { team };
        var teamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var teamListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.InternalServerError
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", teamListResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        // WHEN he clicks on Teams
        // and something went wrong (we couldn't retrieve the data)
        _navMan.NavigateTo("/teams-list-page");

        // THEN he should see the error message “Something went wrong“ at the top of the page.

        Assert.Equal($"http://localhost/error", _navMan.Uri);
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Something went wrong."));
    }

}