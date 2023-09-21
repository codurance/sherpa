namespace SherpaBackEnd.Template.Domain;

public interface ITemplateRepository
{
    Task<Template[]> GetAllTemplatesAsync();
    Task<Template> GetTemplateByName(string templateName);
}