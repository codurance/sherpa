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

    protected bool Equals(SurveyNotification other)
    {
        return Id.Equals(other.Id) && SurveyId.Equals(other.SurveyId) && TeamMemberId.Equals(other.TeamMemberId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SurveyNotification)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, SurveyId, TeamMemberId);
    }

    public static bool operator ==(SurveyNotification? left, SurveyNotification? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SurveyNotification? left, SurveyNotification? right)
    {
        return !Equals(left, right);
    }
}