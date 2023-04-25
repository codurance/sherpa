namespace SherpaFrontEnd.Model;

public class OngoingAssessment
{
    public Guid id;
    public string assessmentName;
    
    public Guid groupId;
    public string groupName;
    
    public Guid templateId;
    public IEnumerable<Survey> surveys;

    public OngoingAssessment(Guid id,String assessmentName, Guid groupId, string groupName, Guid templateId, IEnumerable<Survey> surveys)
    {
        this.id = id;
        this.assessmentName = assessmentName;
        this.groupId = groupId;
        this.groupName = groupName;
        this.templateId = templateId;
        this.surveys = surveys;
    }
}