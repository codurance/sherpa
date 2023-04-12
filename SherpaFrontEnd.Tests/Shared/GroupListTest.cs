using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Shared;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Shared;

public class GroupListTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupsList> _renderedComponent;
    private Mock<IGroupDataService> _mockGroupDataService;

    public GroupListTest()
    {
        _testContext = new TestContext();
        _mockGroupDataService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(p => _mockGroupDataService.Object);
    }

    [Fact]
    public void AsssertThatListOfGroupsIsRendered()
    {
        var group = new Group("Group A");
        _mockGroupDataService.Setup(groupService => groupService.getGroups())
            .Returns(new List<Group> { new Group("Group A") });
        
        _renderedComponent = _testContext.RenderComponent<GroupsList>(b => b.Add(b => b.groups,new List<Group>{group}));
        
        Assert.Equal(group.Name, _renderedComponent.Instance.groups[0].Name);
    }
}