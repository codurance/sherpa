using BlazorApp.Model;
using Bunit;

namespace BlazorApp.Tests;

public class GroupMembersTest
{
 
    [Fact]
    public void AssertFirstElementOfTheListShowsTheRightValues()
    {
        var _groupMember = new GroupMember("Bob","Smith","CEO");
        
        using var context = new TestContext();
        var renderedComponent = context.RenderComponent<Pages.GroupMemberTable>(b => 
            b.Add( e => e.GroupMembers, new List<GroupMember>{_groupMember}));

        Assert.Equal(_groupMember.Name1,renderedComponent.Instance.GroupMembers[0].Name1);
    }
}