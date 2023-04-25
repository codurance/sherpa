using SherpaBackEnd.Model;

namespace SherpaBackEnd.Dtos;

public class Assessment
{
    public Guid GroupId { get; }
    public Guid TemplateId { get; }
    public IEnumerable<Survey> Surveys{ get; }

    public Assessment(Guid groupId, Guid templateId)
    {
        GroupId = groupId;
        TemplateId = templateId;
        Surveys = new List<Survey>();
    }
}