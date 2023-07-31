using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Services;

public class TemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }
}