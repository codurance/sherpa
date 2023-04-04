using Bunit;

namespace BlazorApp.Tests;

public class UnitTest1
{
    [Fact]
    public void shouldTrue()
    {
        Assert.True(true);
    }
    
    [Fact]
    public void Basic_Markup_IndexComponentRendersCorrectly()
    {
        using var context = new TestContext();

        var renderedComponent = context.RenderComponent<Pages.Index>();

        renderedComponent.MarkupMatches(
            "<h1>Hello, world!</h1>\r\n\r\nWelcome to your new app.");
    }
}
