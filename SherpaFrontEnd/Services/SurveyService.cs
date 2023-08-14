using System.Net.Http.Json;
using System.Text.Json;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Model;


namespace SherpaFrontEnd.Services;

public class SurveyService: ISurveyService
{
    private readonly HttpClient _httpClient;
    private const string SherpaBackend = "SherpaBackEnd";

    public SurveyService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(SherpaBackend);
    }

    public async Task<List<Survey>?> GetAllSurveysByTeam(Guid teamId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{teamId}/surveys");
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Survey>>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}