namespace SherpaBackEnd.Template.Application;

public interface ITemplateService
{
    public Task<Domain.Template[]> GetAllTemplatesAsync();
}