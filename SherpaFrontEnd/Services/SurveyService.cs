using System.Net.Http.Json;
using SherpaFrontEnd.Dtos.Survey;


namespace SherpaFrontEnd.Services;

public class SurveyService: ISurveyService
{
    private readonly HttpClient _httpClient;
    private const string SherpaBackend = "SherpaBackEnd";

    public SurveyService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(SherpaBackend);
    }

    public Task<List<Survey>>GetAllSurveysByTeam()
    {
        throw new NotImplementedException();
    }
}