using Bunit;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class GroupListTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupsList> _renderedComponent;

    public GroupListTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void AssertThatListOfGroupsIsRendered()
    {
        var group = new Group
        {
            Name = "Group A",
            Id = Guid.NewGuid()
        };
        
        _renderedComponent = _testContext.RenderComponent<GroupsList>(
            ComponentParameter.CreateParameter("Groups",new List<Group>{group}));
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups[0].Name);
    }
}