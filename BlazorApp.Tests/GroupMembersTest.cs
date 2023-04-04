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
        var _groupMember = new GroupMember("Bob","Smith","CEO");
        _renderedComponent = _testContext.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>{_groupMember}));
        
        Assert.Equal(_groupMember.Name,_renderedComponent.Instance.GroupMembers[0].Name);
    }

    [Fact]
    public void AssertLastNameCanBeEmpty()
    {
        var _groupMember = new GroupMember("Bob",null,"CEO");
        var renderedComponent = _testContext.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>{_groupMember}));

        Assert.Equal(_groupMember.LastName,renderedComponent.Instance.GroupMembers[0].LastName);
    }
    
}