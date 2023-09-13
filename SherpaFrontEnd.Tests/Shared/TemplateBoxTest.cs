using System.Security.Cryptography;

using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class TemplateBoxTest
{
    private readonly TestContext _testContext;
    private readonly FakeNavigationManager _navMan;
    const string TemplatesPage = "templates";

    public TemplateBoxTest()
    {
        _testContext = new TestContext();
        _navMan = _testContext.Services.GetRequiredService<FakeNavigationManager>();

    }

    [Fact]
    public async Task ShouldRedirectToTheTemplateOwnPageOnClick()
    {
        var page = _testContext.RenderComponent<TemplateBox>(ComponentParameter.CreateParameter("Name","Hackman Model"));

        var existingSurveyElement = page.FindElementByCssSelectorAndTextContent("h2", "Hackman Model");
        Assert.NotNull(existingSurveyElement);
        
        existingSurveyElement.Click();
        
        Assert.Equal($"http://localhost/{TemplatesPage}/{Uri.EscapeDataString("Hackman Model")}", _navMan.Uri);
    }
}