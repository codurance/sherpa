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
    private Mock<IGroupDataService> _mockGroupDataService;

    public GroupListTest()
    {
        _testContext = new TestContext();
        _mockGroupDataService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(p => _mockGroupDataService.Object);
    }

    [Fact]
    public void AssertThatListOfGroupsIsRendered()
    {
        var group = new Group("Group A");
        _mockGroupDataService.Setup(groupService => groupService.getGroups())
            .Returns(Task.FromResult(new List<Group> { new Group("Group A") }));
        
        _renderedComponent = _testContext.RenderComponent<GroupsList>();
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups[0].Name);
    }

    [Fact]
    public async Task GroupAreRenderedAsCheckboxes()
    {
        var group = new Group("Group A");
        _mockGroupDataService.Setup(groupService => groupService.getGroups())
            .ReturnsAsync(await Task.FromResult(new List<Group> { group }));

        _renderedComponent = _testContext.RenderComponent<GroupsList>();

        var actualGroupInput = _renderedComponent.Find($"input[id='{group.Id.ToString()}']");

        Assert.Equal(group.Id.ToString(), actualGroupInput.Id);
    }
}