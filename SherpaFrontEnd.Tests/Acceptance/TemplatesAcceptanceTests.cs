using Blazored.Modal;
using Blazored.Modal.Services;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class TemplatesAcceptanceTests
{
    private ITestOutputHelper _outputHelper;
        
    public TemplatesAcceptanceTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void User_navigates_to_templates_page_and_see_hackman_template()
    {
        var ctx = new TestContext();
        ctx.Services.AddBlazoredModal();
        var nav = ctx.Services.GetRequiredService<NavigationManager>();

        var component = ctx.RenderComponent<App>();

        component.Find("a[href='assessments-page']").Click();
        
        component.WaitForState(() => component.Find("label") != null);

        _outputHelper.WriteLine(component.Markup);
    }
    
}