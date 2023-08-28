using BlazorApp.Tests.Helpers.Interfaces;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared.Modals;

namespace BlazorApp.Tests.Shared.Modals;

public class CreateTeamMemberModalTest
{
    private TestContext _ctx;
    private Mock<IGuidService> _mockGuidService;

    public CreateTeamMemberModalTest()
    {
         _ctx = new TestContext();
         _mockGuidService = new Mock<IGuidService>();
         _ctx.Services.AddSingleton<IGuidService>(_mockGuidService.Object);
    }
    
    [Fact]
    public void ShouldShowAllFormFieldsTitleAndButtons()
    {
        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(ComponentParameter.CreateParameter("Show", true));
        
        var addMemberTitle = cut.FindAll("h3")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberTitle);
        
        var addMemberDescription = cut.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add team member by filling in the required information"));
        Assert.NotNull(addMemberDescription);
        
        var fullNameLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = cut.FindAll($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        
        var positionLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Position"));
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.FindAll($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        
        var emailLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Email"));
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.FindAll($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        
        var addMemberButton = cut.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberButton);
        
        var cancelButton = cut.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Cancel"));
        Assert.NotNull(cancelButton);
    }
    
    [Fact]
    public void ShouldCallGivenCreateTeamMemberWithDtoFromDataFilled()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockMethod = new Mock<IWithCreateTeamMember>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);
        
        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true), 
            ComponentParameter.CreateParameter("TeamId", teamId), 
            ComponentParameter.CreateParameter("CreateTeamMember", EventCallback.Factory.Create<AddTeamMemberDto>(this, async (AddTeamMemberDto a) => await mockMethod.Object.CreateTeamMember(a)) )
            );
        
        var fullNameLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Full name"));
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Position"));
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = cut.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Email"));
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = cut.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Add member"));
        Assert.NotNull(addMemberButton);
        
        addMemberButton.Click();
        
        mockMethod.Verify(member => member.CreateTeamMember(new AddTeamMemberDto(teamId, teamMember)));
    }
}