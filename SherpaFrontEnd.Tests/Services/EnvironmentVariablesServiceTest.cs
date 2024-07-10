using System.Net;
using System.Net.Http.Json;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class EnvironmentVariablesServiceTest
{
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _httpClient;

    public EnvironmentVariablesServiceTest()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
    }

    [Fact]
    public async Task ShouldGetEnvironmentVariables()
    {
        var environmentVariables = new EnvironmentVariables("clientId", "authority123");
        var environmentVariablesJson = await JsonContent.Create(environmentVariables).ReadAsStringAsync();

        var environmentVariablesResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(environmentVariablesJson)
        };
        _handlerMock.SetupRequest(HttpMethod.Get, "/configuration", environmentVariablesResponse);

        var environmentVariablesService = new EnvironmentVariablesService(_httpClient);
        var actualEnvironmentVariables = await environmentVariablesService.GetVariables();
        CustomAssertions.StringifyEquals(environmentVariables, actualEnvironmentVariables);
    }
}