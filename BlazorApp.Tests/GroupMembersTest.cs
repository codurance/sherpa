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
    private readonly Mock<IGroupMemberService> _mock;

    public GroupMembersTest()
    {
        _testContext = new TestContext();
        _mock = new Mock<IGroupMemberService>();
        _testContext.Services.AddScoped(p => _mock.Object);
    }

    [Fact]
    public void AssertFirstElementOfTheListShowsTheRightValues()
    {
        var groupMember = new GroupMember("Bob", "Smith", "CEO");
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(b =>
            b.Add(e => e.GroupMembers, new List<GroupMember> { groupMember }));

        Assert.Equal(groupMember.Name, _renderedComponent.Instance.GroupMembers[0].Name);
    }

    [Fact]
    public void AssertMembersRenderedInTheTable()
    {
        var groupMember1 = new GroupMember("Bob", "Smith", "CEO");
        var groupMember2 = new GroupMember("Tom", "Hurdy", "CP");
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(b =>
            b.Add(e => e.GroupMembers, new List<GroupMember>
            {
                groupMember1,
                groupMember2
            }));


        var members = _renderedComponent.FindAll("table>tbody>tr");
        Assert.Equal(groupMember1.Name, members[0].Children[0].InnerHtml);

    }

    [Fact]
    public void AssertThatLastNameCanBeNull()
    {
        var groupMember = new GroupMember("Bob", null, "CEO");
        
        _mock.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember });
        var renderedComponent = _testContext.RenderComponent<GroupMemberTable>();
        
        Assert.Equal(groupMember.LastName, renderedComponent.Instance.GroupMembers[0].LastName);
    }
}