using Bunit;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class TemplateContentTest
{
    [Fact]
    public async Task ShouldDisplayTitleOfTheTempalateAndButtonToLaunchIt()
    {
        var ctx = new TestContext();
        var componenent =
            ctx.RenderComponent<TemplateContent>(ComponentParameter.CreateParameter("TemplateName", "Hackman Model"));
        
        Assert.NotNull(componenent.FindAll("h1").FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model")));
        Assert.NotNull(componenent.FindAll("button").FirstOrDefault(element => element.InnerHtml.Contains("Launch this template")));
    }
}