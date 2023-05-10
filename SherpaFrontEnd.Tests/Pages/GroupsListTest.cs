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
    private Mock<IGroupDataService> _mockGroupService;

    public GroupListTest()
    {
        _testContext = new TestContext();
        _mockGroupService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(s => _mockGroupService.Object);
    }

    [Fact]
    public void AssertThatListOfGroupsIsRendered()
    {
        var group = new Group
        {
            Name = "Group A",
            Id = Guid.NewGuid()
        };
        
        _mockGroupService.Setup(m => m.GetGroups()).ReturnsAsync(new List<Group>{group});
        _renderedComponent = _testContext.RenderComponent<GroupsList>();
        
        Assert.Equal(group.Name, _renderedComponent.Instance.Groups![0].Name);
    }
}