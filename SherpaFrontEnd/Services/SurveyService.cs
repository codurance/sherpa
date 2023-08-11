using System.Text.Json;
using SherpaFrontEnd.Dtos;

namespace SherpaFrontEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string SherpaBackend = "SherpaBackEnd";

    public SurveyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        var request = new HttpRequestMessage(HttpMethod.Post, "/survey");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }

    public async Task<SurveyWithoutQuestions?> GetSurveyById(Guid id)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/survey/{id.ToString()}");
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<SurveyWithoutQuestions>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

}