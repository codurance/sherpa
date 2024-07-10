using Moq;
using SherpaBackEnd.Configuration.Application;
using SherpaBackEnd.Configuration.Domain;

namespace SherpaBackEnd.Tests.Configuration.Application;

public class ConfigurationServiceTest
{

    [Fact]
    public async Task ShouldCallConfigurationRepositoryAndReturnTheConfiguration()
    {
        var expected = new SherpaConfiguration("12345", "http://localhost");

        var mockRepository = new Mock<IConfigurationRepository>();
        mockRepository.Setup(repository => repository.GetConfiguration()).ReturnsAsync(expected);
        var configurationService = new ConfigurationService(mockRepository.Object);

        var actual = await configurationService.GetConfiguration();
        
        Assert.Equal(expected, actual);
    }
}