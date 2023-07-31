namespace SherpaFrontEnd.Services;

public interface ITemplateService
{
    public Task<TemplateWithNameAndTime[]?> GetAllTemplates();
}