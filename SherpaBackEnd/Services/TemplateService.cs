using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Services;

public class TemplateService: ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public Task<Template[]> GetAllTemplates()
    {
        throw new NotImplementedException();
    }
}