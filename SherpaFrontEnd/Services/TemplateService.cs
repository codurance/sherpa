using System.Net.Http.Json;

namespace SherpaFrontEnd.Services;

public class TemplateService: ITemplateService
{
    private readonly HttpClient _httpClient;

    public TemplateService(IHttpClientFactory httpClientFactory)
    {
        const string SherpaBackend = "SherpaBackEnd";
        _httpClient = httpClientFactory.CreateClient(SherpaBackend);
    }


    public Task<TemplateWithNameAndTime[]?> GetAllTemplates()
    {
        return _httpClient.GetFromJsonAsync<TemplateWithNameAndTime[]>("/template");
    }
}