using Moq;
using SherpaBackEnd.ConfigurationVariables.Application;
using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.Tests.ConfigurationVariables.Application;

public class ConfigurationVariablesServiceTest
{

    [Fact]
    public async Task ShouldCallConfigurationVariablesRepositoryAndReturnTheConfigurationVariables()
    {
        var expected = new SherpaConfigurationVariables("12345", "http://localhost");

        var mockRepository = new Mock<IConfigurationVariablesRepository>();
        mockRepository.Setup(repository => repository.GetConfigurationVariables()).ReturnsAsync(expected);
        var configurationVariablesService = new ConfigurationVariablesService(mockRepository.Object);

        var actual = await configurationVariablesService.GetConfigurationVariables();
        
        Assert.Equal(expected, actual);
    }
}