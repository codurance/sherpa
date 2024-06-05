namespace SherpaBackEnd.Survey.Domain;

public class SurveyResponse
{
    public Guid TeamMemberId { get; set; }

    public SurveyResponse(Guid teamMemberId)
    {
        TeamMemberId = teamMemberId;
    }

    protected bool Equals(SurveyResponse other)
    {
        return TeamMemberId.Equals(other.TeamMemberId);
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
        return TeamMemberId.GetHashCode();
    }
}