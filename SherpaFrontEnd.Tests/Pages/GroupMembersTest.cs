using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class GroupMembersTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupMemberTable> _renderedComponent;

    public GroupMembersTest()
    {
        _testContext = new TestContext();
    }

    [Fact]
    public void ElementsOfTheListShowsTheRightValues()
    {
        var groupMember = new GroupMember("Bob", "Smith", "CEO");
        
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "GroupMembers", new List<GroupMember> { groupMember }));

        Assert.Collection(_renderedComponent.Instance.GroupMembers,
            member =>
            {
                Assert.Equal(groupMember.Name, member.Name);
                Assert.Equal(groupMember.LastName, member.LastName);
                Assert.Equal(groupMember.Position, member.Position);
            });
    }
    

    [Fact]
    public void MembersAreRenderedAsTable()
    {
        var groupMember1 = new GroupMember("Tom", "Hardy", "CP");
        var groupMember2 = new GroupMember("Bob", "Smith", "CEO");
        
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "GroupMembers", new List<GroupMember> { groupMember1, groupMember2 }));

        var members = _renderedComponent.FindAll("table>tbody>tr");
        
        Assert.Collection(members,
            member =>
            {
                Assert.Equal(groupMember1.Name, member.Children[0].InnerHtml);
            },
            member =>
            {
                Assert.Equal(groupMember2.Name, member.Children[0].InnerHtml);
            }
            );
    }

    [Fact]
    public async Task ListOfMembersAreAlphabeticallySorted()
    {
        var memberOne = new GroupMember("B_name", "B_lastName","B_position");
        var memberTwo = new GroupMember("A_name", "A_lastName", "A_position");
        var group = new Group
        {
            Name = "Group A",
            Id = Guid.NewGuid()
        };
        group.Members = new List<GroupMember> { memberOne, memberTwo };

        
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter("GroupMembers", group.Members));
        
        Assert.Collection(_renderedComponent.Instance.GroupMembers,
            member =>
            {
                Assert.Equal(memberTwo.LastName, member.LastName);
            },
            member =>
            {
                Assert.Equal(memberOne.LastName, member.LastName);
            }
        );
    }
}