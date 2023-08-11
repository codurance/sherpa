namespace SherpaFrontEnd.Services;

public interface ITemplateService
{
    public Task<TemplateWithoutQuestions[]?> GetAllTemplates();
}