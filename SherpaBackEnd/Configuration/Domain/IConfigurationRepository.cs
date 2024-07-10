namespace SherpaBackEnd.Configuration.Domain;

public interface IConfigurationRepository
{
    Task<SherpaConfiguration> GetConfiguration();
}