using System.Net;
using System.Net.Http.Json;
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

    public TeamsAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
    }
/*
    [Fact]
    async Task should_be_able_to_create_team()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Test Team";
        var team = new Team(teamId, teamName);

        var testCtx = new TestContext();
        var httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var factoryHttpClient = new Mock<IHttpClientFactory>();
        var teamsService = new TeamServiceHttpClient(factoryHttpClient.Object);
        testCtx.Services.AddSingleton<ITeamDataService>(teamsService);
        
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };

        factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);

        var emptyTeamsList = new List<Team>(){new Team(teamId, "monda")};
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };

        httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);

        var teamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var createdTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };
        
        httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Content != null && m.Method.Equals(HttpMethod.Post) && m.Content.Equals(teamJson) && m.RequestUri!.AbsoluteUri.Contains("/team")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(createdTeamResponse);

        var teamsPage = testCtx.RenderComponent<TeamsList>();

        var createTeamButton = teamsPage.Find("#create-team");
        createTeamButton.Click();

        var teamNameFormInput = teamsPage.Find("#create-team-name");
        teamNameFormInput.Change(teamName);

        var confirmTeamCreationButton = teamsPage.Find("#create-team-confirm");
        confirmTeamCreationButton.Click();


        var navMan = testCtx.Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal( $"{baseUrl}/team-content/{teamId}", navMan.Uri);
        
    } */

       
    [Fact]
    private async Task UserShouldBeAbleToNavigateToTeamsPageWithoutTeamsAndSeeItsComponents()
    {
        _testCtx.Services.AddBlazoredModal();
        
        var emptyTeamsList = new List<Team>(){};
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

        var allTeamsTitle = appComponent.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
        
        var createTeamButton = appComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createTeamButton);
    }
    
    [Fact]
    private async Task UserShouldBeAbleToNavigateToTeamsPageWithTeamsAndSeeItsComponents()
    {
        var teamName = "Team name";
        var teamsList = new List<Team>(){new Team(Guid.NewGuid(), teamName)};
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

        var allTeamsTitle = appComponent.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
        
        var createTeamButton = appComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createTeamButton);
        
        var teamNameElement = appComponent.FindAll("h5").FirstOrDefault(element => element.InnerHtml.Contains(teamName));
        Assert.NotNull(teamNameElement);
    }

    [Fact]
    private async Task ShouldBeAbleToSeeCreateNewTeamFormWhenClickingOnCreateNewTeamInTeamsPage()
    {
        // GIVEN that an Org coach is on the All Teams pag

        var emptyTeamsList = new List<Team>(){};
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
        var createNewTeamButton = teamsListComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);
        
        createNewTeamButton.Click();
        
        
        // THEN he should be redirected on the page for creating a team
        //     with one mandatory text field “Team´s name”
        //     and 2 buttons Cancel and Confirm
        var createNewTeamTitle = teamsListComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamTitle);
        var teamNameLabel = teamsListComponent.FindAll("label").FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = teamsListComponent.FindAll($"#{teamNameInputId}");
        Assert.NotNull(teamNameInput);
        var confirmButton = teamsListComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);
        var cancelButton = teamsListComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
        Assert.NotNull(cancelButton);
    }
}