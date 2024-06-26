using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class TeamServiceHttpClientTest
{
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly Mock<IAuthService> _authService;

    public TeamServiceHttpClientTest()
    {
        _httpHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _factoryHttpClient = new Mock<IHttpClientFactory>();
        _authService = new Mock<IAuthService>();
        const string baseUrl = "http://localhost";
        var httpClient = new HttpClient(_httpHandlerMock.Object, false) { BaseAddress = new Uri(baseUrl) };
        _factoryHttpClient.Setup(_ => _.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
    }

    [Fact]
    public async Task ShouldDoAGetHttpCallWhenCallingGetAllTeams()
    {
        var teamsList = new List<Team>() { new Team(Guid.NewGuid(), "Demo team") };
        var teamListJson = await JsonContent.Create(teamsList).ReadAsStringAsync();
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamListJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, "", response);

        var teamService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);

        var serviceResponse = await teamService.GetAllTeams();

        _authService.Verify(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()));
        CustomAssertions.StringifyEquals(teamsList, serviceResponse);
    }

    [Fact]
    public async Task ShouldDoAPostHttpCallWhenCallingAddTeam()
    {
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };

        _httpHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Post)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var teamService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);

        await teamService.AddTeam(new Team(Guid.NewGuid(), "Demo team"));

        _authService.Verify(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()));
        _httpHandlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(
                m => m.Method.Equals(HttpMethod.Post)),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task ShouldDoAGetHttpCallWhenCallingGetTeamById()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var newTeam = new Team(teamId, teamName);
        var teamJson = await JsonContent.Create(newTeam).ReadAsStringAsync();
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };

        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId.ToString()}", response);

        var teamService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);

        var serviceResponse = await teamService.GetTeamById(teamId);

        _authService.Verify(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()));
        CustomAssertions.StringifyEquals(newTeam, serviceResponse);
    }

    [Fact]
    public async Task ShouldDoAPatchHttpCallWhenCallingAddTeamMember()
    {
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Created,
        };

        _httpHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Patch)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var teamService = new TeamServiceHttpClient(_factoryHttpClient.Object, _authService.Object);
        var teamMember = new TeamMember(Guid.NewGuid(), "Sir Alex", "Lazy ass mf", "mymail@dotcom.com");

        await teamService.AddTeamMember(new AddTeamMemberDto(Guid.NewGuid(), teamMember));

        _authService.Verify(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()));
        _httpHandlerMock.Protected().Verify("SendAsync", Times.Once(), ItExpr.Is<HttpRequestMessage>(
                m => m.Method.Equals(HttpMethod.Patch)),
            ItExpr.IsAny<CancellationToken>());
    }
}