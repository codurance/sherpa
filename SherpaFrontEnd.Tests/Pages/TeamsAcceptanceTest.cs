using System.Net;
using System.Net.Http.Json;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TeamsAcceptanceTest
{
    [Fact]
    async Task should_be_able_to_create_team()
    {
        var teamId = new Guid();
        const string teamName = "Test Team";
        var team = new Group(teamId, teamName);
        
        var testCtx = new TestContext();
        var httpHandlerMock = new Mock<HttpMessageHandler>();
        var mockGroupService = new Mock<IGroupDataService>().Setup(m => m.GetGroups())
            .ReturnsAsync(new List<Group> { team });

        var httpClient2 = new Mock<IHttpClientFactory>();
        
        var httpClient = new HttpClient(httpHandlerMock.Object, false) { BaseAddress = new Uri("http://host") };
 
        var teamsService = new GroupServiceHttpClient(httpClient2.Object);
        
        var teamsPage = testCtx.RenderComponent<GroupsList>();

        var emptyTeamsList = new List<Group>();
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
        
        var createTeamButton = teamsPage.Find("#create-team");
        createTeamButton.Click();

        var teamNameFormInput = teamsPage.Find("#create-team-name");
        teamNameFormInput.Change(teamName);

        var confirmTeamCreationButton = teamsPage.Find("#create-team-confirm");
        confirmTeamCreationButton.Click();

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
                    m => m.Content != null && m.Method.Equals(HttpMethod.Post) && m.Content.Equals(teamJson)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(createdTeamResponse);

        var navMan = testCtx.Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal(httpClient.BaseAddress + $"/teams/{teamId}", navMan.Uri);
    }
}