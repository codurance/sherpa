namespace SherpaFrontEnd.Model;

public class Assessment
{
    public string Name;
    
    public Guid GroupId;
    
    public Guid TemplateId;
    public IEnumerable<Survey> Surveys;

    public Assessment(string name, Guid groupId, Guid templateId,
        IEnumerable<Survey> surveys)
    {
        Name = name;
        GroupId = groupId;
        TemplateId = templateId;
        Surveys = surveys;
    }
}