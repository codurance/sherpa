using BlazorApp.Tests.Helpers.Interfaces;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages.TeamContent.Components;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class MemberTableTest
{
    private TestContext _testContext;
    private readonly Mock<IGuidService> _mockGuidService;

    public MemberTableTest()
    {
        _testContext = new TestContext();
        _mockGuidService = new Mock<IGuidService>();
        _testContext.Services.AddSingleton<IGuidService>(_mockGuidService.Object);
    }

    [Fact]
    public void ShouldDisplayCorrectHeadingsAndButtons()
    {
        const string teamName = "Imaginary Team";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);

        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team)
        );

        var teamMembersHeading = membersTableComponent.FindAll("h2.text-title-h3");
        Assert.NotNull(teamMembersHeading.FirstOrDefault(heading => heading.ToMarkup().Contains(team.Name)));

        var removeTeamMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button[disabled]", "Remove");
        Assert.NotNull(removeTeamMemberButton);

        var addTeamMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        var memberFullName = membersTableComponent.FindElementByCssSelectorAndTextContent("th", "Full name");
        var memberEmail = membersTableComponent.FindElementByCssSelectorAndTextContent("th", "E-mail");
        var memberPosition = membersTableComponent.FindElementByCssSelectorAndTextContent("th", "Position");

        Assert.NotNull(memberFullName);
        Assert.NotNull(memberEmail);
        Assert.NotNull(memberPosition);
    }

    [Fact]
    public void ShouldDisplayCorrectTeamMembersDataInMembersTabPageTable()
    {
        const string teamName = "Team with members";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);

        var teamMembers = new List<TeamMember>()
        {
            new TeamMember(Guid.NewGuid(), "Rick Astley", "Popular Youtuber", "rickandrolling@gmail.com")
        };

        team.Members = teamMembers;

        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team)
        );

        var teamMembersRows = membersTableComponent.FindAll("tbody tr");
        Assert.Equal(teamMembers.Count, teamMembersRows.Count);

        foreach (var member in teamMembers)
        {
            Assert.NotNull(
                teamMembersRows.FirstOrDefault(element => element.ToMarkup().Contains(member.FullName), null));
            Assert.NotNull(teamMembersRows.FirstOrDefault(element => element.ToMarkup().Contains(member.Email), null));
            Assert.NotNull(
                teamMembersRows.FirstOrDefault(element => element.ToMarkup().Contains(member.Position), null));
        }
    }

    [Fact]
    public void ShouldDisplayModalWhenClickingOnAddTeamMemberButton()
    {
        const string teamName = "Team with members";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);


        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team)
        );

        var addTeamMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        membersTableComponent.WaitForAssertion(() => Assert.Null(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));

        addTeamMemberButton.Click();

        membersTableComponent.WaitForAssertion(() => Assert.NotNull(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));
    }

    [Fact]
    public void ShouldCloseModalWhenClickingX()
    {
        const string teamName = "Team with members";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        
        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team)
        );

        var addTeamMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        membersTableComponent.WaitForAssertion(() => Assert.Null(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));

        addTeamMemberButton.Click();

        membersTableComponent.WaitForAssertion(() => Assert.NotNull(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));


        var closeModalButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Close modal");

        closeModalButton.Click();

        membersTableComponent.WaitForAssertion(() => Assert.Null(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));
    }

    [Fact]
    public void ShouldCloseModalWhenSubmitingData()
    {
        const string teamName = "Team with members";
        var teamId = Guid.NewGuid();
        var team = new Team(teamId, teamName);
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();

        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team),
            ComponentParameter.CreateParameter("CreateTeamMember", EventCallback.Factory.Create<AddTeamMemberDto>(this,
                async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))
            )
        );

        var addTeamMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        addTeamMemberButton.Click();

        membersTableComponent.WaitForAssertion(() => Assert.NotNull(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));

        var fullNameLabel = membersTableComponent.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = membersTableComponent.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = membersTableComponent.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = membersTableComponent.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = membersTableComponent.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = membersTableComponent.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = membersTableComponent.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        addMemberButton.Click();

        membersTableComponent.WaitForAssertion(() => Assert.Null(membersTableComponent.FindElementByCssSelectorAndTextContent("h3", "Add member")));
    }
}