namespace SherpaFrontEnd.Dtos.Survey;

public class SurveyResponse
{
    public Guid TeamMemberId { get; set; }
    public List<QuestionResponse> Answers { get; set; }

    public SurveyResponse(Guid teamMemberId, List<QuestionResponse> answers)
    {
        TeamMemberId = teamMemberId;
        Answers = answers;
    }

    protected bool Equals(SurveyResponse other)
    {
        return TeamMemberId.Equals(other.TeamMemberId) && Answers.Equals(other.Answers);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SurveyResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TeamMemberId, Answers);
    }

    public static bool operator ==(SurveyResponse? left, SurveyResponse? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SurveyResponse? left, SurveyResponse? right)
    {
        return !Equals(left, right);
    }
}