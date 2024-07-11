using System.Text.Json;
using SherpaFrontEnd.Dtos;

namespace SherpaFrontEnd.Services;

public class EnvironmentVariablesService
{
    private readonly HttpClient _httpClient;

    public EnvironmentVariablesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnvironmentVariables?> GetVariables()
    {
        var httpRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, "/configuration-variables");

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<EnvironmentVariables>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}