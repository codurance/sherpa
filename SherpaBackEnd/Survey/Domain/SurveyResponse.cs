namespace SherpaBackEnd.Survey.Domain;

public class SurveyResponse
{
    public Guid TeamMemberId { get; set; }

    public SurveyResponse(Guid teamMemberId)
    {
        TeamMemberId = teamMemberId;
    }
}