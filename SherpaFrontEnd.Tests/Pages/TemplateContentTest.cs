using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class TemplateContentTest
{
    private FakeNavigationManager _navMan;
    private readonly TestContext _ctx;

    public TemplateContentTest()
    {
        _ctx = new TestContext();
        _navMan = _ctx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task ShouldDisplayTitleOfTheTempalateAndButtonToLaunchIt()
    {
        var component =
            _ctx.RenderComponent<TemplateContent>(ComponentParameter.CreateParameter("TemplateName", "Hackman Model"));
        
        Assert.NotNull(component.FindAll("h1").FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model")));
        Assert.NotNull(component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Launch this template")));
    }
    
    [Fact]
    public async Task ShouldRedirectToDeliverySettingsPageWhenClickingOnLaunchThisTemplate()
    {
        var component =
            _ctx.RenderComponent<TemplateContent>(ComponentParameter.CreateParameter("TemplateName", "Hackman Model"));

        var launchButton = component.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Launch this template"));
        Assert.NotNull(launchButton);
        
        launchButton.Click();
        
        component.WaitForAssertion(() => Assert.Equal($"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}", _navMan.Uri));
    }
}