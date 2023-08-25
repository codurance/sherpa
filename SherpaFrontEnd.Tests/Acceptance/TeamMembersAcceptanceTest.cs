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
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class TeamMembersAcceptanceTest
{
    private readonly TestContext _testCtx;
    private Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly Mock<ISurveyService> _surveyService;
    private readonly TeamServiceHttpClient _teamsService;
    private FakeNavigationManager _navMan;
    private ITestOutputHelper _output;
    private readonly Mock<IGuidService> _guidService;

    public TeamMembersAcceptanceTest(ITestOutputHelper output)
    {
        _output = output;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object);
        _guidService = new Mock<IGuidService>();
        _surveyService = new Mock<ISurveyService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        
        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    private async Task ShouldBeAbleToSeeAddTeamMemberFormWhenClickingOnAddMemberInTeamsPageAndMembersTab()
    {
        // GIVEN that an Org coach is on the Team page (Members tab)
        // WHEN he clicks on “Add coachee“
        // THEN he should be redirected on the Adding members page
        // and  see the following info:
        // Full name - text field - mandatory
        // email (email should be validated) - mandatory
        // Position - text field - mandatory


        // GIVEN that an Org coach is on the Team page (Members tab)

        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamListJson = await JsonContent.Create(team).ReadAsStringAsync();
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
                    m => m.Method.Equals(HttpMethod.Get) &&
                         m.RequestUri.AbsoluteUri.EndsWith($"/team/{teamId.ToString()}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(teamListResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        _navMan.NavigateTo($"/team-content/{teamId}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{teamId}", _navMan.Uri));
        
        var membersTab = appComponent.FindAll("a:not(a[href])")
            .FirstOrDefault(element => element.InnerHtml.Contains("Members"));
        
        Assert.NotNull(membersTab);
        membersTab.Click();

        // WHEN he clicks on “Add member“
        
        appComponent.WaitForAssertion(() =>
            Assert.NotNull(appComponent.FindAll("button")
                .FirstOrDefault(element => element.InnerHtml.Contains("Add member"))));
        
        var addTeamMemberButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addTeamMemberButton);
        
        addTeamMemberButton.Click();

        // THEN he should be redirected on the Adding members page
        // and  see the following info:
            // Full name - text field - mandatory
            // email (email should be validated) - mandatory
            // Position - text field - mandatory
            
        var addMemberTitle = appComponent.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberTitle);
        
        var addMemberDescription = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add team member by filling in the required information"));
        Assert.NotNull(addMemberDescription);
        
        var fullNameLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = appComponent.FindAll($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        
        var positionLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Position"));
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = appComponent.FindAll($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        
        var emailLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Email"));
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = appComponent.FindAll($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        
        var addMemberButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberButton);
        
        var cancelButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
        Assert.NotNull(cancelButton);
    }
}