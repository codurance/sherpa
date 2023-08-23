﻿using Bunit;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;

namespace BlazorApp.Tests.Pages;

public class MemberTableTest
{
    private TestContext _testContext;

    public MemberTableTest()
    {
        _testContext = new TestContext();
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

        var memberFullName = membersTableComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var memberEmail = membersTableComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("E-mail"));
        var memberPosition = membersTableComponent.FindAll("th").FirstOrDefault(element => element.InnerHtml.Contains("Position"));
        
        Assert.NotNull(memberFullName);
        Assert.NotNull(memberEmail);
        Assert.NotNull(memberPosition);
    }
}