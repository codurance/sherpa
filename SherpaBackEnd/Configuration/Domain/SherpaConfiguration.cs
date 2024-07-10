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
}