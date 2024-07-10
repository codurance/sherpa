using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Configuration.Application;
using SherpaBackEnd.Configuration.Domain;
using SherpaBackEnd.Configuration.Infrastructure.Http;

namespace SherpaBackEnd.Tests.Configuration.Infrastructure.Http;

public class ConfigurationControllerTest
{
    
    private Mock<IConfigurationService> _serviceMock;
    private ConfigurationController _controller;

    public ConfigurationControllerTest()
    {
        _serviceMock = new Mock<IConfigurationService>();
        _controller = new ConfigurationController(_serviceMock.Object);
    }
    
    [Fact]
    public async void ShouldCallConfigurationService()
    {
        await _controller.GetConfig();
        
        _serviceMock.Verify(service => service.GetConfiguration());
    }
    
    [Fact]
    public async void ShouldReturnOkObjectResultWithSherpaConfigurationWhenGetConfigSucceeds()
    {
        var expected = new SherpaConfiguration("12345", "http://localhost");
        _serviceMock.Setup(service => service.GetConfiguration()).ReturnsAsync(expected);
        
        var response = await _controller.GetConfig();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var configuration = Assert.IsType<SherpaConfiguration>(resultObject.Value);
        Assert.Equal(expected, configuration);
    }
}