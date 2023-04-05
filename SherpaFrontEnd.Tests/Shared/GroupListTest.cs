using Bunit;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Shared;

namespace BlazorApp.Tests.Shared;

public class GroupListTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupList> _renderedComponent;

    public GroupListTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void AsssertThatListOfGroupsIsRendered()
    {
        var group = new Group{Name = "group1"};
        
        _renderedComponent = _testContext.RenderComponent<GroupList>(b => b.Add(b => b.Groups,new List<Group>{group}));
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups[0].Name);
    }
}