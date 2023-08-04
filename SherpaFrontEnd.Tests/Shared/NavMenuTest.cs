using Bunit;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class NavMenuTest
{
    [Fact]
    public void Should_have_an_anchor_with_templates_ref()
    {
        var ctx = new TestContext();
        var component = ctx.RenderComponent<NavMenu>();

        var element = component.Find("a[href='templates']");
        
        Assert.NotNull(element);
    }
}