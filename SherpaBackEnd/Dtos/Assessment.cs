using SherpaBackEnd.Model;

namespace SherpaBackEnd.Dtos;

public class Assessment
{
    public Guid GroupId { get; }
    public Guid TemplateId { get; }
    
    public string Name { get; }
    public IEnumerable<Survey> Surveys{ get; }

    public Assessment(Guid groupId, Guid templateId, string name)
    {
        GroupId = groupId;
        TemplateId = templateId;
        Name = name;
        Surveys = new List<Survey>();
    }
}