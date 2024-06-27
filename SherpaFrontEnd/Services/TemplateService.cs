using System.Net.Http.Json;
using System.Text.Json;

namespace SherpaFrontEnd.Services;

public class TemplateService: ITemplateService
{
    private readonly IAuthService _authService;
    private readonly HttpClient _httpClient;
    private const string SherpaBackend = "SherpaBackEnd";

    public TemplateService(IHttpClientFactory httpClientFactory, IAuthService authService)
    {
        _authService = authService;
        _httpClient = httpClientFactory.CreateClient(SherpaBackend);
    }

    public async Task<TemplateWithoutQuestions[]?> GetAllTemplates()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/template");
        request = await _authService.DecorateWithToken(request);
        var response = await _httpClient.SendAsync(request);

        var responseString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TemplateWithoutQuestions[]>(
            responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}