using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class GroupsPageTest
{
 
    private readonly TestContext _testContext;
    private Mock<IGroupDataService> _mockDataService;
    private IRenderedComponent<GroupsPage> _renderedComponent;

    public GroupsPageTest()
    {
        _testContext = new TestContext();
        _mockDataService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(p => _mockDataService.Object);
    }
}