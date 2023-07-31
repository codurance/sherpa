namespace SherpaFrontEnd.Services;

public class TemplateService: ITemplateService
{
    private readonly HttpClient _httpClient;

    public TemplateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public Task GetAllTemplates()
    {
        throw new NotImplementedException();
    }
}