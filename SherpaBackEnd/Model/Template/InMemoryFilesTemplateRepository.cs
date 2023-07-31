namespace SherpaBackEnd.Model.Template;

public class InMemoryFilesTemplateRepository : ITemplateRepository
{
    private readonly string _folder;

    public InMemoryFilesTemplateRepository(string folder)
    {
        _folder = folder;
    }

    public Task<Template[]> GetAllTemplates()
    {
        throw new NotImplementedException();
    }
}