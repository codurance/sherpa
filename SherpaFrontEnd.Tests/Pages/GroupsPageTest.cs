using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
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

    [Fact]
    public async Task ListOfMembersAreAlphabeticallySorted()
    {
        var memberOne = new GroupMember("B_name", "B_lastName","B_position");
        var memberTwo = new GroupMember("A_name", "A_lastName", "A_position");
        var group = new Group("Group A");
        group.Members = new List<GroupMember> { memberOne, memberTwo };

        _mockDataService.Setup(d => d.getGroups())
            .ReturnsAsync(new List<Group> { group });
        
        _renderedComponent = _testContext.RenderComponent<GroupsPage>();

        var firstMemberLastName = _renderedComponent.Instance.Groups[0].Members[0].LastName;
        var secondMemberLastName = _renderedComponent.Instance.Groups[0].Members[1].LastName;
        
        Assert.Equal(memberTwo.LastName, firstMemberLastName);
        Assert.Equal(memberOne.LastName, secondMemberLastName);
    }
}