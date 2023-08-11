using System.Net;
using System.Net.Http.Json;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Acceptance;

public class SurveysAcceptanceTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<IGuidService> _guidService;
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly FakeNavigationManager _navMan;

    public SurveysAcceptanceTest()
    {
        _testCtx = new TestContext();
        _guidService = new Mock<IGuidService>();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }
    
    [Fact]
    public async Task ShouldBeAbleToSeeTeamSurveysFromANewTeam()
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

        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.Contains($"/team")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(emptyTeamsListResponse);
        
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
        
        var singleTeamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
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
                         m.RequestUri.AbsoluteUri.Contains($"/team/{newTeamId.ToString()}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(singleTeamResponse);

        var emptySurveyList = new List<Survey>() {};
        var emptySurveyListJson = await JsonContent.Create(emptySurveyList).ReadAsStringAsync();
        var emptySurveyListResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptySurveyListJson)
        };
        
        _httpHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.Contains($"/team/{newTeamId.ToString()}/surveys")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(emptySurveyListResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        
        var teamsListPage = "teams-list-page";
        var teamsPageLink = appComponent.Find($"a[href='{teamsListPage}']");
        Assert.NotNull(teamsPageLink);

        _navMan.NavigateTo($"/{teamsListPage}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/teams-list-page", _navMan.Uri));

        var createNewTeamButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Create new team"));
        Assert.NotNull(createNewTeamButton);

        createNewTeamButton.Click();
        
        var teamNameLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team's name"));
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        teamNameInput.Change(newTeamName);
        
        var confirmButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Confirm"));
        Assert.NotNull(confirmButton);

        confirmButton.Click();
        
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{newTeamId.ToString()}", _navMan.Uri));
        
        var teamSurveysTabPage = appComponent.FindAll("a:not(a[href])")
            .FirstOrDefault(element => element.InnerHtml.Contains("Surveys"));
        Assert.NotNull(teamSurveysTabPage);
        
        teamSurveysTabPage.Click();
        
        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindAll("h3").FirstOrDefault(element => element.InnerHtml.Contains("All Surveys"))));
        Assert.NotNull(appComponent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Send a new survey")));
        Assert.NotNull(appComponent.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("Currently there is not any survey for your team")));
    }
}

public class Survey
{
}
