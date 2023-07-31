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

namespace BlazorApp.Tests.Pages;

public class TeamsAcceptanceTest
{
    [Fact]
    async Task should_be_able_to_create_team()
    {
        var testCtx = new TestContext();
        var teamName = "Test Team";

        var handlerMock = new Mock<HttpMessageHandler>();

        var testTeamId = new Guid();
        var testTeam = new Group(testTeamId, "Team test name");
        var testTeamJson = await JsonContent.Create(testTeam).ReadAsStringAsync();
        
        var createdTeamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };

        var emptyTeamsList = new List<Group>();
        var emptyTeamListJson = await JsonContent.Create(emptyTeamsList).ReadAsStringAsync();
        var responseEmpty = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamListJson)
        };
        handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseEmpty);
        
        handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Post)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(createdTeamResponse);
        
        

        var httpClient = new HttpClient(handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        
        

        var teamsPage = testCtx.RenderComponent<GroupsList>(
            parameters => parameters.Add(p => p.GoToUrl, httpClient.BaseAddress + "/teams/:id"));


        var createTeamButton = teamsPage.Find("#create-team");
        createTeamButton.Click();

        var teamNameFormInput = teamsPage.Find("#create-team-name");
        teamNameFormInput.Change(teamName);

        var confirmTeamCreationButton = teamsPage.Find("#create-team-confirm");
        confirmTeamCreationButton.Click();

        var actualTeamName = teamsPage.FindAll("h3").FirstOrDefault(title => title.InnerHtml.Contains(teamName));
        Assert.NotNull(actualTeamName);

        var navMan = testCtx.Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal("http://localhost/foo", navMan.Uri);
    }
}