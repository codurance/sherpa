using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using Blazored.Modal;
using Blazored.Modal.Services;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared.Modals;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Pages;

public class TeamsAcceptanceTest
{
    private ITestOutputHelper output;

    public TeamsAcceptanceTest(ITestOutputHelper output)
    {
        this.output = output;
    }

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
        
    }

    [Fact]
    async Task shouldBeAbleToNavigateToTeamsPageAndSeeItsComponents()
    {
        var testCtx = new TestContext();
        testCtx.Services.AddBlazoredModal();

        var httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var factoryHttpClient = new Mock<IHttpClientFactory>();
        var teamsService = new TeamServiceHttpClient(factoryHttpClient.Object);
        testCtx.Services.AddSingleton<ITeamDataService>(teamsService);

        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);

        var emptyTeamsList = new List<Team>(){};
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
        
        var appComponent = testCtx.RenderComponent<App>();


        var targetPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{targetPage}']");
        Assert.NotNull(teamsPageLink);
        var navManager = testCtx.Services.GetRequiredService<FakeNavigationManager>();
        navManager.NavigateTo($"/{targetPage}");
        output.WriteLine(appComponent.Markup);

        var allTeamsTitle = appComponent.FindAll("h1,h2,h3").FirstOrDefault(element => element.InnerHtml.Contains("All teams"));
        Assert.NotNull(allTeamsTitle);
        
        var createTeamButton = appComponent.Find("#create-team");
        Assert.NotNull(createTeamButton);
    }
}