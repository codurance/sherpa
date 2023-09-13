using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Template.Application;

public class TemplateService: ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<Domain.Template[]> GetAllTemplatesAsync()
    {
        return await _templateRepository.GetAllTemplatesAsync();
    }
}