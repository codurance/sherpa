﻿using AngleSharp.Dom;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
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

        var teamMembersHeading = membersTableComponent.FindAll("h2.text-title-h2--semibold");
        Assert.NotNull(teamMembersHeading.FirstOrDefault(heading => heading.ToMarkup().Contains(team.Name)));

        var removeTeamMemberButton = membersTableComponent.FindAll("button[disabled]")
            .FirstOrDefault(button => button.ToMarkup().Contains("Remove"));
        Assert.NotNull(removeTeamMemberButton);

        var addTeamMemberButton = membersTableComponent.FindAll("button")
            .FirstOrDefault(button => button.ToMarkup().Contains("Add member"));
        Assert.NotNull(addTeamMemberButton);

        var memberFullName = membersTableComponent.FindAll("th")
            .FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var memberEmail = membersTableComponent.FindAll("th")
            .FirstOrDefault(element => element.InnerHtml.Contains("E-mail"));
        var memberPosition = membersTableComponent.FindAll("th")
            .FirstOrDefault(element => element.InnerHtml.Contains("Position"));

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

        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team),
            ComponentParameter.CreateParameter("Members", teamMembers)
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

        var teamMembers = new List<TeamMember>();

        var membersTableComponent = _testContext.RenderComponent<MemberTable>(
            ComponentParameter.CreateParameter("Team", team),
            ComponentParameter.CreateParameter("Members", teamMembers)
        );
        
        var addTeamMemberButton = membersTableComponent.FindAll("button")
            .FirstOrDefault(button => button.ToMarkup().Contains("Add member"));
        Assert.NotNull(addTeamMemberButton);
        
        membersTableComponent.WaitForAssertion(() => Assert.Null(membersTableComponent.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"))));
        
        addTeamMemberButton.Click();
        
        membersTableComponent.WaitForAssertion(() => Assert.NotNull(membersTableComponent.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"))));
    }
}