using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class GuidServiceTest
{
    [Fact]
    public void ShouldReturnAGuidWhenGeneratingOne()
    {
        var guidService = new GuidService();

        var generatedRandomGuid = guidService.GenerateRandomGuid();
        
        Assert.IsType<Guid>(generatedRandomGuid);
    }
}