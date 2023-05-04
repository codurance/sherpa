namespace SherpaFrontEnd.Model;

public class Assessment
{
    public string Name { get; set; }

    public Guid GroupId { get; set; }

    public Guid TemplateId { get; set; }
    public IEnumerable<Survey> Surveys { get; set; }

    public Assessment(string name, Guid groupId, Guid templateId)
    {
        Name = name;
        GroupId = groupId;
        TemplateId = templateId;
        Surveys = new List<Survey>();
    }
}