using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TemplatesTest
{
    [Fact]
    public void Component_should_print_the_result_of_calling_getAllTemplates_of_service()
    {
        var ctx = new TestContext();
        var mockService = new Mock<ITemplateService>();
        var templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
        mockService.Setup(service => service.GetAllTemplates()).ReturnsAsync(templates);
        ctx.Services.AddSingleton<ITemplateService>(mockService.Object);
        var component = ctx.RenderComponent<Templates>();
        
        var elementWithText = component.FindAll("h2").FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        
        Assert.NotNull(elementWithText);
    }
}