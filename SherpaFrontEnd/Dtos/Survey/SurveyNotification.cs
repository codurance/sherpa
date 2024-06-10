namespace SherpaFrontEnd.Dtos.Survey;

public class SurveyNotification
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid TeamMemberId { get; set; }

    public SurveyNotification(Guid id, Guid surveyId, Guid teamMemberId)
    {
        Id = id;
        SurveyId = surveyId;
        TeamMemberId = teamMemberId;
    }
}