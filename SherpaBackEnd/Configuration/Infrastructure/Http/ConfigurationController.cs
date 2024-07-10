using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Configuration.Application;
using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Configuration.Infrastructure.Http;

[ApiController]
[Route("")]
public class ConfigurationController
{

    private readonly IConfigurationService _configurationService;

    public ConfigurationController(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    public async Task<ActionResult<SherpaConfiguration>> GetConfig()
    {
        var sherpaConfiguration = await _configurationService.GetConfiguration();
        return new OkObjectResult(sherpaConfiguration);
    }
}