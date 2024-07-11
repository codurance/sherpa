namespace SherpaBackEnd.ConfigurationVariables.Domain;

public class SherpaConfigurationVariables
{
    public string CognitoClientId { get; }
    public string CognitoAuthority { get; }
    
    public SherpaConfigurationVariables(string cognitoClientId, string cognitoAuthority)
    {
        CognitoClientId = cognitoClientId;
        CognitoAuthority = cognitoAuthority;
    }

    protected bool Equals(SherpaConfigurationVariables other)
    {
        return CognitoClientId == other.CognitoClientId && CognitoAuthority == other.CognitoAuthority;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SherpaConfigurationVariables)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CognitoClientId, CognitoAuthority);
    }
}