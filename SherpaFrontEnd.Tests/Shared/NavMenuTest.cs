using AngleSharp.Css.Dom;
using AngleSharp.Dom;
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
    public void ShouldHaveAnAnchorWithTeamsPageHRefRAndRedirectToTeamsList()
    {
        var ctx = new TestContext();
        var component = ctx.RenderComponent<NavMenu>();

        var teamsElement = component.FindAll("a")
            .FirstOrDefault(element => element.InnerHtml.Contains("Teams"));
        Assert.NotNull(teamsElement);
        Assert.Contains("teams-list-page", teamsElement.GetAttribute("href"));
    }

}