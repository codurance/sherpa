namespace SherpaBackEnd.Model.Template;

public interface ITemplateRepository
{
    Task<Template[]> GetAllTemplatesAsync();
}