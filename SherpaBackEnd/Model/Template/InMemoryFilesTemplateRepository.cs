using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Model;

public class InMemoryFilesTemplateRepository : ITemplateRepository
{
    private readonly string _folder;

    public InMemoryFilesTemplateRepository(string folder)
    {
        _folder = folder;
    }
}