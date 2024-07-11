namespace SherpaBackEnd.ConfigurationVariables.Domain;

public interface IConfigurationVariablesRepository
{
    Task<SherpaConfigurationVariables> GetConfigurationVariables();
}