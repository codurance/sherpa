using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.ConfigurationVariables.Application;
using SherpaBackEnd.ConfigurationVariables.Domain;
using SherpaBackEnd.ConfigurationVariables.Infrastructure.Http;

namespace SherpaBackEnd.Tests.ConfigurationVariables.Infrastructure.Http;

public class ConfigurationVariablesControllerTest
{
    
    private Mock<IConfigurationVariablesService> _serviceMock;
    private ConfigurationVariablesController _configurationVariablesController;

    public ConfigurationVariablesControllerTest()
    {
        _serviceMock = new Mock<IConfigurationVariablesService>();
        _configurationVariablesController = new ConfigurationVariablesController(_serviceMock.Object);
    }
    
    [Fact]
    public async void ShouldCallConfigurationService()
    {
        await _configurationVariablesController.GetConfigurationVariables();
        
        _serviceMock.Verify(service => service.GetConfigurationVariables());
    }
    
    [Fact]
    public async void ShouldReturnOkObjectResultWithSherpaConfigurationWhenGetConfigSucceeds()
    {
        var expected = new SherpaConfigurationVariables("12345", "http://localhost");
        _serviceMock.Setup(service => service.GetConfigurationVariables()).ReturnsAsync(expected);
        
        var response = await _configurationVariablesController.GetConfigurationVariables();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var configurationVariables = Assert.IsType<SherpaConfigurationVariables>(resultObject.Value);
        Assert.Equal(expected, configurationVariables);
    }
}