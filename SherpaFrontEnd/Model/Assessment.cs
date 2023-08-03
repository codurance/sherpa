namespace SherpaFrontEnd.Model;

public class Assessment
{
    public string Name { get; set; }

    public Guid TeamId { get; set; }

    public Guid TemplateId { get; set; }
    public IEnumerable<Survey> Surveys { get; set; }

    public Assessment(string name, Guid teamId, Guid templateId)
    {
        Name = name;
        TeamId = teamId;
        TemplateId = templateId;
        Surveys = new List<Survey>();
    }
}