using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.ConfigurationVariables.Application;

public interface IConfigurationVariablesService
{
    Task<SherpaConfigurationVariables> GetConfigurationVariables();
}