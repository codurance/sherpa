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
        var groupMember = new GroupMember("Bob", "Smith", "CEO", "email@mail.com");

        var group = new Group
        {
            Members = new List<GroupMember>{groupMember}
        };
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "Group", group));

        Assert.Collection(_renderedComponent.Instance.Group.Members,
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
        var groupMember1 = new GroupMember("Tom", "Hardy", "CP", "email1@mail.com");
        var groupMember2 = new GroupMember("Bob", "Smith", "CEO", "email2@mail.com");

        var group = new Group
        {
            Members = new List<GroupMember>
            {
                groupMember1,
                groupMember2
            }
        };
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "Group", group));

        var members = _renderedComponent.FindAll("table>tbody>tr");
        
        Assert.Equal(2, members.Count);
    }

    [Fact]
    public async Task ListOfMembersAreAlphabeticallySorted()
    {
        var memberOne = new GroupMember("B_name", "B_lastName","B_position", "emailB@mail.com");
        var memberTwo = new GroupMember("A_name", "A_lastName", "A_position", "emailA@mail.com");
        var group = new Group
        {
            Name = "Group A",
            Id = Guid.NewGuid()
        };
        group.Members = new List<GroupMember> { memberOne, memberTwo };

        
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter("Group", group));
        
        Assert.Collection(_renderedComponent.Instance.Group.Members,
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