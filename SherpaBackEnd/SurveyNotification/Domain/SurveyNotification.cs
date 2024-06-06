using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.SurveyNotification.Domain;

public class SurveyNotification
{
    public Guid Id { get; }
    public Survey.Domain.Survey Survey { get; }
    public TeamMember TeamMember { get;  }

    public SurveyNotification(Guid id, Survey.Domain.Survey survey, TeamMember teamMember)
    {
        Id = id;
        Survey = survey;
        TeamMember = teamMember;
    }

    protected bool Equals(SurveyNotification other)
    {
        return Id.Equals(other.Id) && Survey.Equals(other.Survey) && TeamMember.Equals(other.TeamMember);
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
        return HashCode.Combine(Id, Survey, TeamMember);
    }
}