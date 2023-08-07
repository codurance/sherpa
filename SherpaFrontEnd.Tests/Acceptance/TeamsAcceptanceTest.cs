using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AngleSharp.Dom;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
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
    private readonly Mock<IAssessmentsDataService> _assessmentsService;

    public TeamsAcceptanceTest(ITestOutputHelper output)
    {
        _output = output;

        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object);
        _guidService = new Mock<IGuidService>();
        _assessmentsService = new Mock<IAssessmentsDataService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _testCtx.Services.AddSingleton<IAssessmentsDataService>(_assessmentsService.Object);
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);

        var appComponent = _testCtx.RenderComponent<App>();


        var targetPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{targetPage}']");
        Assert.NotNull(teamsPageLink);
        var navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
        navManager.NavigateTo($"/{targetPage}");

        var allTeamsTitle = appComponent.FindAll("h1,h2,h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);

        var createTeamButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var appComponent = _testCtx.RenderComponent<App>();


        var targetPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{targetPage}']");
        Assert.NotNull(teamsPageLink);
        var navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
        navManager.NavigateTo($"/{targetPage}");

        var allTeamsTitle = appComponent.FindAll("h1,h2,h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);

        var createTeamButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createTeamButton);

        var teamNameElement =
            appComponent.FindAll("h5").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);

        var teamsListComponent = _testCtx.RenderComponent<TeamsList>();

        // WHEN he clicks on “+ Create a new team“
        var createNewTeamButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();


        // THEN he should be redirected on the page for creating a team
        //     with one mandatory text field “Team´s name”
        //     and 2 buttons Cancel and Confirm
        var createNewTeamTitle = teamsListComponent.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamTitle);
        var teamNameLabel = teamsListComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.FindAll($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        var confirmButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);
        var cancelButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);

        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Post)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(creationResponse);

        var teamsListComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.Contains($"/team/{teamId.ToString()}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(teamResponse);

        var teamNameLabel = teamsListComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        var confirmButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);

        confirmButton.Click();

        //    THEN he should be redirected on this Team page (Analysis tab)
        //and on the Team page he should see the following info:
        //Team name without Editing
        //Tab Analysis
        //the button “Send a new survey“ without functionality

        Assert.Equal($"http://localhost/team-content/{teamId.ToString()}", _navMan.Uri);

        _navMan.NavigateTo($"/team-content/{teamId.ToString()}");

        _output.WriteLine(_navMan.Uri);
        _output.WriteLine(teamsListComponent.Markup);
        teamsListComponent.WaitForState(() =>
            teamsListComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains(teamName)) != null);


        var teamNameElement = teamsListComponent.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains(teamName));
        var analysisTab = teamsListComponent.FindAll("li")
            .FirstOrDefault(element => element.InnerHtml.Contains("Analysis"));
        var sendNewSurveyTeam = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Send a new survey"));

        Assert.NotNull(teamNameElement);
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

        var createNewTeamButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        // WHEN he enters a team name

        var teamId = Guid.NewGuid();

        _guidService.Setup(service => service.GenerateRandomGuid()
        ).Returns(teamId);

        var teamNameLabel = teamsListComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        // and clicks on Cancel

        var cancelButton = teamsListComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(teamListResponse);

        var singleTeamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.Contains($"/team/{teamId.ToString()}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(singleTeamResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        // and he can click on this team

        var existingTeamNameElement =
            appComponent.FindAll("h5").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(teamListResponse);

        var singleTeamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var singleTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(singleTeamJson)
        };

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.Contains($"/team/{teamId.ToString()}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(singleTeamResponse);

        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.InternalServerError,
        };

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Post)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(creationResponse);

        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();

        // WHEN he enters Teams name

        var teamNameLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change("Demo team");

        //     and click on Confirm

        var confirmButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);

        confirmButton.Click();

        //     and something went wrong (we could not create a new team)
        // THEN he should see the error message “Something went wrong“ at the top of the page

        Assert.Equal($"http://localhost/error", _navMan.Uri);
        Assert.NotNull(appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains("Something went wrong.")));
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(teamListResponse);
        
        var appComponent = _testCtx.RenderComponent<App>();

        _navMan.NavigateTo("/teams-list-page");

        var createNewTeamButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();
        
        // WHEN he clicks on Confirm
        // and he didn't enter anything to the mandatory field Teams name

        var confirmButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);

        confirmButton.Click();
        
        appComponent.WaitForElement(".validation-message");

        // THEN this field should be highlighted in read and at the top of the page he should see an error message that it's mandatory field.
        
        var teamNameLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));

        var inputGroup = teamNameLabel.Parent;


        Assert.Contains("This field is mandatory", inputGroup.ToMarkup());
    }
}