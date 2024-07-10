using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Configuration.Application;
using SherpaBackEnd.Configuration.Domain;
using SherpaBackEnd.Configuration.Infrastructure.Http;
using SherpaBackEnd.Configuration.Infrastructure.Persistence;

namespace SherpaBackEnd.Tests.Acceptance;

public class ConfigurationAcceptanceTest
{
    [Fact]
    public async void ShouldBeAbleToRetrieveConfiguration()
    {
        var environmentVariablesService = new EnvironmentVariablesConfigurationRepository();
        var configurationService = new ConfigurationService(environmentVariablesService);
        
        var configurationController = new ConfigurationController(configurationService);

        var cognitoClientId = "12345";
        var cognitoAuthority = "http://localhost";
        
        var expected = new SherpaConfiguration(cognitoClientId, cognitoAuthority);
        
        var response = await configurationController.GetConfig();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var configuration = Assert.IsType<SherpaConfiguration>(resultObject.Value);
        Assert.Equal(expected, configuration);
    }
    
}