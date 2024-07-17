using System.Net;
using System.Net.Http.Json;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class AnalysisServiceTest
{
    private readonly Mock<HttpMessageHandler> _httpHandlerMock;
    private readonly Mock<IHttpClientFactory> _factoryHttpClient;
    private readonly Mock<IAuthService> _authService;

    public AnalysisServiceTest()
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
    public async Task ShouldDoAGetHttpCallWhenCallingGetGeneralResults()
    {
        var teamId = Guid.NewGuid();
        var generalResults = AnalysisHelper.BuildGeneralResultsDto();

        var generalResultsJson = await JsonContent.Create(generalResults).ReadAsStringAsync();
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(generalResultsJson)
        };
        
        _httpHandlerMock.SetupRequest(HttpMethod.Get, $"/team/{teamId}/analysis/general-results", response);

        var analysisService = new AnalysisService(_factoryHttpClient.Object, _authService.Object);

        var serviceResponse = await analysisService.GetGeneralResults(teamId);
        
        _authService.Verify(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()));
        CustomAssertions.StringifyEquals(generalResults, serviceResponse);
    }
}