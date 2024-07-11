using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.ConfigurationVariables.Application;

public class ConfigurationVariablesService : IConfigurationVariablesService
{
    private readonly IConfigurationVariablesRepository _configurationVariablesRepository;

    public ConfigurationVariablesService(IConfigurationVariablesRepository configurationVariablesRepository)
    {
        _configurationVariablesRepository = configurationVariablesRepository;
    }

    public async Task<SherpaConfigurationVariables> GetConfigurationVariables()
    {
        return await _configurationVariablesRepository.GetConfigurationVariables();
    }
}