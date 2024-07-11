namespace SherpaFrontEnd.Dtos;

public class EnvironmentVariables
{
    public string CognitoClientId { get; set; }
    public string CognitoAuthority { get; set; }

    public EnvironmentVariables(string cognitoClientId, string cognitoAuthority)
    {
        CognitoClientId = cognitoClientId;
        CognitoAuthority = cognitoAuthority;
    }
}