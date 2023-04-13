using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

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
        var group = new Group("Group A");
        
        _renderedComponent = _testContext.RenderComponent<GroupsList>(
            ComponentParameter.CreateParameter("Groups",new List<Group>{group}));
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups[0].Name);
    }

    [Fact]
    public async Task GroupAreRenderedAsCheckboxes()
    {
        var group = new Group("Group A");
        _renderedComponent = _testContext.RenderComponent<GroupsList>(
            ComponentParameter.CreateParameter("Groups",new List<Group>{group}));

        var actualGroupInput = _renderedComponent.Find($"input[id='{group.Id.ToString()}']");

        Assert.Equal(group.Id.ToString(), actualGroupInput.Id);
    }
}