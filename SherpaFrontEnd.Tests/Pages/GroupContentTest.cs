using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class GroupContentTest
{
    private TestContext _testContext;
    private IRenderedComponent<GroupContent> _renderedComponent;
    private Mock<IGroupDataService> _mockGroupService;

    public GroupContentTest()
    {
        _testContext = new TestContext();
        _mockGroupService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(s => _mockGroupService.Object);
    }

    [Fact]
    public async Task SingleGroupIsRendered()
    {
        var group = new Group
        {
            Name = "Group A",
            Id = Guid.NewGuid()
        };

        _mockGroupService.Setup(m => m.GetGroup(It.IsAny<Guid>())).ReturnsAsync(group);
        _renderedComponent = _testContext.RenderComponent<GroupContent>();

        Assert.Equal(group.Name, _renderedComponent.Instance.Group.Name);
    }
}