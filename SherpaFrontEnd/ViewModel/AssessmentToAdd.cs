namespace SherpaFrontEnd.ViewModel;

public class AssessmentToAdd
{
    public Guid TeamId { get; set; }

    public Guid TemplateId { get; set; }

    public string Name { get; set; }
    
    public AssessmentToAdd(Guid teamId, Guid templateId, string name)
    {
        TeamId = teamId;
        TemplateId = templateId;
        Name = name;
    }
}