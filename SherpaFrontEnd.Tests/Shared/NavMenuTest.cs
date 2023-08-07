using Bunit;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class NavMenuTest
{
    [Fact]
    public void ShouldHaveAnAnchorWithTemplatesRef()
    {
        var ctx = new TestContext();
        var component = ctx.RenderComponent<NavMenu>();

        var element = component.Find("a[href='templates']");
        
        Assert.NotNull(element);
    }
    
    [Fact]
    public void ShouldHaveAnAnchorWithTeamsPageRef()
    {
        var ctx = new TestContext();
        var component = ctx.RenderComponent<NavMenu>();

        var element = component.Find("a[href='teams-list-page']");

        Assert.NotNull(element);
    }

    [Fact]
    public void ShouldHaveTheTitleTeams()
    {
        var ctx = new TestContext();
        var component = ctx.RenderComponent<NavMenu>();

        var element = component.Find("a[href='teams-list-page']");

        Assert.Contains("Teams", element.InnerHtml);
    }
}