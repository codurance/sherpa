using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        var cognitoClientId = "12345";
        var cognitoAuthority = "http://localhost";
        var expected = new SherpaConfiguration(cognitoClientId, cognitoAuthority);
        var mockConfiguration = new SherpaConfiguration(cognitoClientId, cognitoAuthority);
        var configurationRepository = new Mock<IConfigurationRepository>();
        configurationRepository.Setup(repository => repository.GetConfiguration()).ReturnsAsync(mockConfiguration);
        var configurationService = new ConfigurationService(configurationRepository.Object);
        
        var configurationController = new ConfigurationController(configurationService);
        
        var response = await configurationController.GetConfig();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var configuration = Assert.IsType<SherpaConfiguration>(resultObject.Value);
        Assert.Equal(expected, configuration);
    }
    
}