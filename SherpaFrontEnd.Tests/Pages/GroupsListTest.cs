using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class GroupsListTest
{
    private readonly TestContext _testContext;
    private IRenderedComponent<GroupsList> _renderedComponent;
    private readonly Mock<IGroupDataService> _dataService;

    public GroupsListTest()
    {
        var groups = new List<Group> { new Group("Group A") };

        _testContext = new TestContext();
        _dataService = new Mock<IGroupDataService>();

        _dataService.Setup(dataService => dataService.getGroups())
            .Returns(Task.FromResult(groups)!);
        
        _testContext.Services.AddScoped(p => _dataService.Object);
    }

    [Fact]
    public async Task ListComponentRendersProperly()
    {
        var group = new Group("Group A");
        _renderedComponent = _testContext.RenderComponent<GroupsList>();
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups[0].Name);
    }
}