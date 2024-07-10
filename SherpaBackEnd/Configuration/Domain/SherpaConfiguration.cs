namespace SherpaBackEnd.Configuration.Domain;

public class SherpaConfiguration
{
    
    public string CognitoClientId { get; init; }
    public string CognitoAuthority { get; init; }
    public SherpaConfiguration(string cognitoClientId, string cognitoAuthority)
    {
        CognitoClientId = cognitoClientId;
        CognitoAuthority = cognitoAuthority;
    }

    protected bool Equals(SherpaConfiguration other)
    {
        return CognitoClientId == other.CognitoClientId && CognitoAuthority == other.CognitoAuthority;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SherpaConfiguration)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CognitoClientId, CognitoAuthority);
    }
}