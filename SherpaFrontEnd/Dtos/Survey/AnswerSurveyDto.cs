namespace SherpaFrontEnd.Dtos.Survey;

public class AnswerSurveyDto
{
    public Guid TeamMemberId { get; set; }
    public Guid SurveyId { get; set; }
    public SurveyResponse Response { get; set; }

    public AnswerSurveyDto(Guid teamMemberId, Guid surveyId, SurveyResponse surveyResponse)
    {
        TeamMemberId = teamMemberId;
        SurveyId = surveyId;
        Response = surveyResponse;
    }

    protected bool Equals(AnswerSurveyDto other)
    {
        return TeamMemberId.Equals(other.TeamMemberId) && SurveyId.Equals(other.SurveyId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AnswerSurveyDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TeamMemberId, SurveyId);
    }

    public static bool operator ==(AnswerSurveyDto? left, AnswerSurveyDto? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AnswerSurveyDto? left, AnswerSurveyDto? right)
    {
        return !Equals(left, right);
    }
}