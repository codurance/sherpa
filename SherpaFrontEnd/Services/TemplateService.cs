using System.Net.Http.Json;

namespace SherpaFrontEnd.Services;

public class TemplateService: ITemplateService
{
    private readonly HttpClient _httpClient;
    private const string SherpaBackend = "SherpaBackEnd";

    public TemplateService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(SherpaBackend);
    }

    public Task<TemplateWithNameAndTime[]?> GetAllTemplates()
    {
        return _httpClient.GetFromJsonAsync<TemplateWithNameAndTime[]>("/template");
    }
}