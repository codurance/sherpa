using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Configuration.Infrastructure.Persistence;

public class EnvironmentVariablesConfigurationRepository : IConfigurationRepository
{
    public Task<SherpaConfiguration> GetConfiguration()
    {
        throw new NotImplementedException();
    }
}