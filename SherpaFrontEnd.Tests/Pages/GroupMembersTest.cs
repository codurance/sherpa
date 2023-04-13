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

        //todo: refactor to use Assert.collection
        Assert.Equal(groupMember.Name, _renderedComponent.Instance.GroupMembers[0].Name);
        Assert.Equal(groupMember.LastName, _renderedComponent.Instance.GroupMembers[0].LastName);
        Assert.Equal(groupMember.Position, _renderedComponent.Instance.GroupMembers[0].Position);
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
        Assert.Equal(groupMember1.Name, members[0].Children[0].InnerHtml);
        Assert.Equal(groupMember2.Name, members[1].Children[0].InnerHtml);
    }

    [Fact]
    public void TableHasTheProperHeaders()
    {
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "GroupMembers", new List<GroupMember>{new ("asdf","qwer","zxcv")}));

        var headers = _renderedComponent.FindAll("table>thead>tr>th");
        Assert.Equal("Name", headers[0].InnerHtml);
        Assert.Equal("Last Name", headers[1].InnerHtml);
        Assert.Equal("Position", headers[2].InnerHtml);
    }

    [Fact]
    public void TableLoadsWithNullMemberList()
    {
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

        var headers = _renderedComponent.FindAll("table>thead>tr>th");
        Assert.Equal("Name", headers[0].InnerHtml);
        Assert.Equal("Last Name", headers[1].InnerHtml);
        Assert.Equal("Position", headers[2].InnerHtml);
    }

    [Fact]
    public void TableLoadsWithEmptyMemberList()
    {
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>(
            ComponentParameter.CreateParameter(
                "GroupMembers", Enumerable.Empty<GroupMember>().ToList()));

        var headers = _renderedComponent.FindAll("table>thead>tr>th");
        Assert.Equal("Name", headers[0].InnerHtml);
        Assert.Equal("Last Name", headers[1].InnerHtml);
        Assert.Equal("Position", headers[2].InnerHtml);
    }
}