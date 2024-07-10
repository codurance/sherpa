using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Configuration.Infrastructure.Persistence;

public class EnvironmentVariablesConfigurationRepository : IConfigurationRepository
{
    public Task<SherpaConfiguration> GetConfiguration()
    {
        var cognitoClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
        var cognitoAuthority = Environment.GetEnvironmentVariable("COGNITO_AUTHORITY");
        
        var sherpaConfiguration = new SherpaConfiguration(
            cognitoClientId ?? string.Empty,
            cognitoAuthority ?? string.Empty
        );
        
        return Task.FromResult(sherpaConfiguration);
    }
}