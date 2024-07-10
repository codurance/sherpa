using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Configuration.Application;

public interface IConfigurationService
{
    Task<SherpaConfiguration> GetConfiguration();
}