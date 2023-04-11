using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

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
    public void ElementsOfTheListShowsTheRightValues()
    {
        var groupMember = new GroupMember("Bob", "Smith", "CEO");
        _mockGroupMemberService.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

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
        _mockGroupMemberService.Setup(m => m.GetMembers()).Returns(new List<GroupMember> { groupMember1,groupMember2 });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

        var members = _renderedComponent.FindAll("table>tbody>tr");
        Assert.Equal(groupMember1.Name, members[0].Children[0].InnerHtml);
        Assert.Equal(groupMember2.Name, members[1].Children[0].InnerHtml);
    }

    [Fact]
    public void TableHasTheProperHeaders()
    {
        _mockGroupMemberService.Setup(m => m.GetMembers())
            .Returns(new List<GroupMember>{new GroupMember("asdf","qwer","zxcv")});
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

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
        _mockGroupMemberService.Setup(m => m.GetMembers())
            .Returns(Enumerable.Empty<GroupMember>().ToList);
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

        var headers = _renderedComponent.FindAll("table>thead>tr>th");
        Assert.Equal("Name", headers[0].InnerHtml);
        Assert.Equal("Last Name", headers[1].InnerHtml);
        Assert.Equal("Position", headers[2].InnerHtml);
    }

    [Fact]
    public void ListOfMembersInTheAlphabeticalOrder()
    {
        var groupMember1 = new GroupMember("Bob", "Smith", "CEO");
        var groupMember2 = new GroupMember("Tom", "Hardy", "CP");
        var groupMember3 = new GroupMember("Mike", "Fox", "CP");
        _mockGroupMemberService.Setup(m => m.GetMembers())
            .Returns(new List<GroupMember> { groupMember1, groupMember2, groupMember3 });
        _renderedComponent = _testContext.RenderComponent<GroupMemberTable>();

        //todo: refactor to use Assert.collection
        var members = _renderedComponent.FindAll("table>tbody>tr");
        Assert.Equal(groupMember3.LastName, members[0].Children[1].InnerHtml);
        Assert.Equal(groupMember2.LastName, members[1].Children[1].InnerHtml);
        Assert.Equal(groupMember1.LastName, members[2].Children[1].InnerHtml);
    }
}