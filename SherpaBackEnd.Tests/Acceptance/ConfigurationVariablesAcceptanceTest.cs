using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.ConfigurationVariables.Application;
using SherpaBackEnd.ConfigurationVariables.Domain;
using SherpaBackEnd.ConfigurationVariables.Infrastructure.Http;

namespace SherpaBackEnd.Tests.Acceptance;

public class ConfigurationVariablesAcceptanceTest
{
    [Fact]
    public async void ShouldBeAbleToRetrieveConfigurationVariables()
    {
        var cognitoClientId = "12345";
        var cognitoAuthority = "http://localhost";
        var expected = new SherpaConfigurationVariables(cognitoClientId, cognitoAuthority);
        var mockConfiguration = new SherpaConfigurationVariables(cognitoClientId, cognitoAuthority);
        var configurationVariablesRepository = new Mock<IConfigurationVariablesRepository>();
        configurationVariablesRepository.Setup(repository => repository.GetConfigurationVariables()).ReturnsAsync(mockConfiguration);
        var configurationVariablesService = new ConfigurationVariablesService(configurationVariablesRepository.Object);
        
        var configurationController = new ConfigurationVariablesController(configurationVariablesService);
        
        var response = await configurationController.GetConfigurationVariables();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var configurationVariables = Assert.IsType<SherpaConfigurationVariables>(resultObject.Value);
        Assert.Equal(expected, configurationVariables);
    }
    
}