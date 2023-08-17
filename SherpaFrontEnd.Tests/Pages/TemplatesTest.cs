using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class TemplatesTest
{
    private readonly Mock<ITemplateService> _mockService;
    private readonly TestContext _testContext;
    private readonly FakeNavigationManager _navMan;
    private TemplateWithoutQuestions[] _templates;
    const string TemplatesPage = "templates";

    public TemplatesTest()
    {
        _testContext = new TestContext();
        _mockService = new Mock<ITemplateService>();
        _testContext.Services.AddBlazoredModal();
        _testContext.Services.AddSingleton<ITemplateService>(_mockService.Object);
        _navMan = _testContext.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public void ComponentShouldPrintTheResultOfCallingGetAllTemplatesOfService()
    {
        _templates = new[] { new TemplateWithoutQuestions("Hackman Model", 30) };
        _mockService.Setup(service => service.GetAllTemplates()).ReturnsAsync(_templates);
        var component = _testContext.RenderComponent<Templates>();

        var titleElement = component.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        var timeElement = component.FindAll("p").FirstOrDefault(element => element.InnerHtml.Contains("30 min"));


        Assert.NotNull(titleElement);
        Assert.NotNull(timeElement);
    }

    [Fact]
    public async Task ShouldBeAbleToClickOnATemplateAndNavigateToItsOwnPage()
    {
        _templates = new[] { new TemplateWithoutQuestions("Hackman Model", 30) };
        _mockService.Setup(service => service.GetAllTemplates()).ReturnsAsync(_templates);
        var page = _testContext.RenderComponent<Templates>();

        var existingSurveyElement = page.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        Assert.NotNull(existingSurveyElement);

        existingSurveyElement.Click();

        Assert.Equal($"http://localhost/{TemplatesPage}/{Uri.EscapeDataString("Hackman Model")}", _navMan.Uri);
    }
}