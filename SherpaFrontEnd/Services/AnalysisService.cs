using System.Text.Json;
using SherpaFrontEnd.Dtos.Analysis;

namespace SherpaFrontEnd.Services;

public class AnalysisService : IAnalysisService
{
    private const string Sherpabackend = "SherpaBackEnd";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthService _authService;

    public AnalysisService(IHttpClientFactory httpClientFactory, IAuthService authService)
    {
        _httpClientFactory = httpClientFactory;
        _authService = authService;
    }

    public async Task<GeneralResultsDto> GetGeneralResults(Guid teamId)
    {
        var client = _httpClientFactory.CreateClient(Sherpabackend);
        var request = new HttpRequestMessage(HttpMethod.Get, $"/team/{teamId}/analysis/general-results");
        request = await _authService.DecorateWithToken(request);
        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<GeneralResultsDto>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}