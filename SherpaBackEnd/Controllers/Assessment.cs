using SherpaBackEnd.Model;

namespace SherpaBackEnd.Controllers;

public class Assessment
{
    public Guid GroupId;
    public Guid TemplateId;
    public IEnumerable<Survey> Surveys;

    public Assessment(Guid groupId, Guid templateId)
    {
        GroupId = groupId;
        TemplateId = templateId;
        Surveys = new List<Survey>();
    }
}