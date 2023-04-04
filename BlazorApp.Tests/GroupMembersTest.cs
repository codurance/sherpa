using BlazorApp.Model;
using BlazorApp.Pages;
using Bunit;

namespace BlazorApp.Tests;

public class GroupMembersTest
{

    private TestContext _testContext;
    private IRenderedComponent<GroupMemberTable> _renderedComponent;

    public GroupMembersTest()
    {
        _testContext = new TestContext();
    }
    
    [Fact]
    public void AssertFirstElementOfTheListShowsTheRightValues()
    {
        var groupMember = new GroupMember("Bob","Smith","CEO");
        _renderedComponent = _testContext.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>{groupMember}));
        
        Assert.Equal(groupMember.Name,_renderedComponent.Instance.GroupMembers[0].Name);
    }

    [Fact]
    public void AssertLastNameCanBeEmpty()
    {
        var groupMember1 = new GroupMember("Bob","Smith","CEO");
        var groupMember2 = new GroupMember("Tom","Hurdy","CP");
        var renderedComponent = _testContext.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>
            {
                groupMember1, 
                groupMember2
            }));

        var members = renderedComponent.FindAll("table>tbody>tr");
        Assert.Equal(groupMember1.Name, members[0].Children[0].InnerHtml);
        
    }

    [Fact]
    public void AssertThatGroupMemberRenderedInTheTable()
    {
        var groupMember = new GroupMember("Bob",null,"CEO");
        var renderedComponent = _testContext.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>{groupMember}));
    }
    
}