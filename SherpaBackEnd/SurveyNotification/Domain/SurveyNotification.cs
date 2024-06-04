namespace SherpaBackEnd.SurveyNotification.Domain;

public class SurveyNotification
{
    private readonly Guid _surveyId;
    private readonly Guid _janeId;
    private readonly Guid _id;

    public SurveyNotification(Guid id, Guid surveyId, Guid janeId)
    {
        _id = id;
        _surveyId = surveyId;
        _janeId = janeId;
    }

    protected bool Equals(SurveyNotification other)
    {
        return _surveyId.Equals(other._surveyId) && _janeId.Equals(other._janeId) && _id.Equals(other._id);
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
        return HashCode.Combine(_surveyId, _janeId, _id);
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