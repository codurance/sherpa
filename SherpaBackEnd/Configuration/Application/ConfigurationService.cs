using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Configuration.Application;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfigurationRepository _configurationRepository;

    public ConfigurationService(IConfigurationRepository configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }

    public async Task<SherpaConfiguration> GetConfiguration()
    {
        throw new NotImplementedException();
    }
}