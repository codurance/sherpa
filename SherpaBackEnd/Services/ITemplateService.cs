using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Services;

public interface ITemplateService
{
    public Task<Template[]> GetAllTemplatesAsync();
}