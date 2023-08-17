using System.Net.Http.Json;
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

        await client.PostAsJsonAsync("/survey", createSurveyDto);
    }

    public async Task<SurveyWithoutQuestions?> GetSurveyById(Guid id)
    {
        var client = _httpClientFactory.CreateClient(SherpaBackend);
        return await client.GetFromJsonAsync<SurveyWithoutQuestions>($"/survey/{id.ToString()}");
    }

}