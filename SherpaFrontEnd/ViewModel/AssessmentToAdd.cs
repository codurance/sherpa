namespace SherpaFrontEnd.ViewModel;

public class AssessmentToAdd
{
    public Guid GroupId { get; set; }

    public Guid TemplateId { get; set; }

    public string Name { get; set; }
    
    public AssessmentToAdd(Guid groupId, Guid templateId, string name)
    {
        GroupId = groupId;
        TemplateId = templateId;
        Name = name;
    }
}