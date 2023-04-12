using Bunit;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Shared;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Shared;

public class GroupListTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupsList> _renderedComponent;

    public GroupListTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void AsssertThatListOfGroupsIsRendered()
    {
        var group = new Group("group1");
        
        _renderedComponent = _testContext.RenderComponent<GroupsList>(b => b.Add(b => b.groups,new List<Group>{group}));
        
        Assert.Equal(group.Name, _renderedComponent.Instance.groups[0].Name);
    }
}