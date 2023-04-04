using BlazorApp.Model;
using BlazorApp.Pages;
using BlazorApp.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BlazorApp.Tests;

public class GroupMembersTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupMemberTable> _renderedComponent;
    private readonly Mock<IGroupMemberService> _mockGroupMemberService;

    public GroupMembersTest()
    {
        _testContext = new TestContext();
        _mockGroupMemberService = new Mock<IGroupMemberService>();
        _testContext.Services.AddScoped(p => _mockGroupMemberService.Object);
    }

    [Fact]
    public void AssertFirstElementOfTheListShowsTheRightValues()
    {
        var groupMember = new GroupMember("Bob", "Smith", "CEO");
        _mockGroupMemberService.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(b =>
            b.Add(e => e.GroupMembers, new List<GroupMember> { groupMember }));

        Assert.Equal(groupMember.Name, _renderedComponent.Instance.GroupMembers[0].Name);
    }
    
    [Fact]
    public void AssertThatLastNameCanBeNull()
    {
        var groupMember = new GroupMember("Bob", null, "CEO");
        _mockGroupMemberService.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();
        
        Assert.Equal(groupMember.LastName, _renderedComponent.Instance.GroupMembers[0].LastName);
    }

    [Fact]
    public void AssertMembersRenderedInTheTable()
    {
        var groupMember1 = new GroupMember("Bob", "Smith", "CEO");
        var groupMember2 = new GroupMember("Tom", "Hurdy", "CP");
        _mockGroupMemberService.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember1,groupMember2 });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

        var members = _renderedComponent.FindAll("table>tbody>tr");
        Assert.Equal(groupMember1.Name, members[0].Children[0].InnerHtml);
        Assert.Equal(groupMember2.Name, members[1].Children[0].InnerHtml);
    }
}