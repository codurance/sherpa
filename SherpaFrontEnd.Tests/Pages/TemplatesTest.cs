using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TemplatesTest
{
    private readonly Mock<ITemplateService> _mockService;

    public TemplatesTest()
    {
        _mockService = new Mock<ITemplateService>();
    }

    [Fact]
    public void Component_should_print_the_result_of_calling_getAllTemplates_of_service()
    {
        var ctx = new TestContext();
        var templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
        _mockService.Setup(service => service.GetAllTemplates()).ReturnsAsync(templates);
        ctx.Services.AddSingleton<ITemplateService>(_mockService.Object);
        var component = ctx.RenderComponent<Templates>();
        
        var titleElement = component.FindAll("h2").FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        var timeElement = component.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("30 min"));
        
        Assert.NotNull(titleElement);
        Assert.NotNull(timeElement);
    }
}