using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.SurveyNotification.Domain;

public class SurveyNotification
{
    private readonly Survey.Domain.Survey _survey;
    private readonly TeamMember _teamMember;
    private readonly Guid _id;

    public SurveyNotification(Guid id, Survey.Domain.Survey survey, TeamMember teamMember)
    {
        _id = id;
        _survey = survey;
        _teamMember = teamMember;
    }

    protected bool Equals(SurveyNotification other)
    {
        return _id.Equals(other._id);
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
        return _id.GetHashCode();
    }
}