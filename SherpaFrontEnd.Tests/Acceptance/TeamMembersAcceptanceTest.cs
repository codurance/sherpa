using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using AngleSharp.Dom;
using Blazored.LocalStorage;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class TeamMembersAcceptanceTest
{
    private readonly TestContext _testCtx;
    private Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly ISurveyService _surveyService;
    private readonly TeamServiceHttpClient _teamsService;
    private FakeNavigationManager _navMan;
    private ITestOutputHelper _output;
    private readonly Mock<IGuidService> _guidService;
    private Mock<IAuthService> _authService;

    public TeamMembersAcceptanceTest(ITestOutputHelper output)
    {
        _output = output;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _teamsService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);
        _surveyService = new SurveyService(_factoryHttpClient.Object, _authService.Object);
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamsService);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new []{new Claim("username", "Demo user")});

        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    private async Task ShouldBeAbleToSeeAddTeamMemberFormWhenClickingOnAddMemberInTeamsPageAndMembersTab()
    {
        // GIVEN that an Org coach is on the Team page (Members tab)

        const string teamName = "Team name";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamJson = await JsonContent.Create(team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };

        var creationResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };

        var teamWithNewMember = new Team(teamId, teamName);
        var teamMember =
            new TeamMember(Guid.NewGuid(), "Nick Choudhry", "CraftsPerson-In-Training", "demo@codurance.es");
        teamWithNewMember.Members.Add(teamMember);

        var teamWithNewMemberJson = await JsonContent.Create(teamWithNewMember).ReadAsStringAsync();

        var teamWithNewMemberResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamWithNewMemberJson)
        };
        
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}", teamResponse, teamWithNewMemberResponse);

        var emptySurveyResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[]")
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}/surveys", emptySurveyResponse);
        
        _httpHandlerMock.SetupRequest(HttpMethod.Patch, "", creationResponse);

        var appComponent = _testCtx.RenderComponent<App>();
        _navMan.NavigateTo($"/team-content/{teamId}");
        appComponent.WaitForAssertion(() => Assert.Equal($"http://localhost/team-content/{teamId}", _navMan.Uri));

        var membersTab = appComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Members");

        Assert.NotNull(membersTab);
        membersTab.Click();

        // WHEN he clicks on “Add member“

        appComponent.WaitForAssertion(() =>
            Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("button", "Add member")));

        var addTeamMemberButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        addTeamMemberButton.Click();

        // THEN he should be redirected on the Adding members page
        // and  see the following info:
        // Full name - text field - mandatory
        // email (email should be validated) - mandatory
        // Position - text field - mandatory

        var addMemberTitle = appComponent.FindElementByCssSelectorAndTextContent("h3", "Add member");
        Assert.NotNull(addMemberTitle);

        var addMemberDescription = appComponent.FindElementByCssSelectorAndTextContent("p", "Add team member by filling in the required information");
        Assert.NotNull(addMemberDescription);

        var fullNameLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = appComponent.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = appComponent.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = appComponent.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);
        
        // AND WHEN clicking on Add member

        addMemberButton.Click();
        
        appComponent.WaitForAssertion(() =>  Assert.Null(appComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));
        
        // THEN the created member should be in the members table

        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("td", teamMember.FullName)));
        
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("td", teamMember.Position));
        
        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("td", teamMember.Email));
    }
}