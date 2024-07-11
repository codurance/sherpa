using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.ConfigurationVariables.Application;
using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.ConfigurationVariables.Infrastructure.Http;

[ApiController]
[Route("configuration-variables")]
public class ConfigurationVariablesController
{

    private readonly IConfigurationVariablesService _configurationVariablesService;

    public ConfigurationVariablesController(IConfigurationVariablesService configurationVariablesService)
    {
        _configurationVariablesService = configurationVariablesService;
    }

    [HttpGet]
    public async Task<ActionResult<SherpaConfigurationVariables>> GetConfigurationVariables()
    {
        var configurationVariables = await _configurationVariablesService.GetConfigurationVariables();
        return new OkObjectResult(configurationVariables);
    }
}