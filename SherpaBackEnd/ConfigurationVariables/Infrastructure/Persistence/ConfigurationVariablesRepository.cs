using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.ConfigurationVariables.Infrastructure.Persistence;

public class ConfigurationVariablesRepository : IConfigurationVariablesRepository
{
    public Task<SherpaConfigurationVariables> GetConfigurationVariables()
    {
        var cognitoClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
        var cognitoAuthority = Environment.GetEnvironmentVariable("COGNITO_AUTHORITY");
        
        var sherpaConfiguration = new SherpaConfigurationVariables(
            cognitoClientId ?? string.Empty,
            cognitoAuthority ?? string.Empty
        );
        
        return Task.FromResult(sherpaConfiguration);
    }
}