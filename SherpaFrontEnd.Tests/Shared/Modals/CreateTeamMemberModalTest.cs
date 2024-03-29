
using BlazorApp.Tests.Helpers.Interfaces;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared.Modals;

namespace BlazorApp.Tests.Shared.Modals;

public class CreateTeamMemberModalTest
{
    private TestContext _ctx;
    private Mock<IGuidService> _mockGuidService;
    private readonly FakeNavigationManager _navMan;

    public CreateTeamMemberModalTest()
    {
        _ctx = new TestContext();
        _mockGuidService = new Mock<IGuidService>();
        _ctx.Services.AddSingleton<IGuidService>(_mockGuidService.Object);
        _navMan = _ctx.Services.GetService<FakeNavigationManager>();
    }

    [Fact]
    public void ShouldShowAllFormFieldsTitleAndButtons()
    {
        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(ComponentParameter.CreateParameter("Show", true));

        var addMemberTitle = cut.FindElementByCssSelectorAndTextContent("h3", "Add member");
        Assert.NotNull(addMemberTitle);

        var addMemberDescription = cut.FindElementByCssSelectorAndTextContent("p", "Add team member by filling in the required information");
        Assert.NotNull(addMemberDescription);

        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = cut.FindAll($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.FindAll($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.FindAll($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);

        var addMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        var cancelButton = cut.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);
    }

    [Fact]
    public void ShouldCallGivenCreateTeamMemberWithDtoFromDataFilledAndClose()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        addMemberButton.Click();

        var mockMethodInvocations = mockWithCreateTeamMember.Invocations[0];

        Assert.Equal("CreateTeamMember", mockMethodInvocations.Method.Name);
        CustomAssertions.StringifyEquals(new AddTeamMemberDto(teamId, teamMember),
            (AddTeamMemberDto)mockMethodInvocations.Arguments[0]);

        mockWithCloseModal.Verify(mock => mock.CloseModal(), Times.Once);
    }

    [Fact]
    public void ShouldCallCloseModalIfXButtonClicked()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var closeModalButton = cut.FindElementByCssSelectorAndTextContent("button", "Close modal");
        Assert.NotNull(closeModalButton);

        closeModalButton.Click();

        mockWithCloseModal.Verify(mock => mock.CloseModal(), Times.Once);
    }
    
    [Fact]
    public void ShouldCallCloseModalIfCancelButtonClicked()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var cancelButton = cut.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);

        cancelButton.Click();

        mockWithCloseModal.Verify(mock => mock.CloseModal(), Times.Once);
    }

    [Fact]
    public void ShouldBeEmptyAfterSubmitting()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        addMemberButton.Click();

        cut.WaitForAssertion(() =>
        {
            fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
            fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
            teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
            Assert.NotNull(teamMemberNameInput);
            Assert.Equal("", teamMemberNameInput.GetAttribute("value"));
        });
        
        positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        positionInputId = positionLabel.Attributes.GetNamedItem("for");
        positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);

        emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        emailInputId = emailLabel.Attributes.GetNamedItem("for");
        emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);

        Assert.Equal("", positionInput.GetAttribute("value"));
        Assert.Equal("", emailInput.GetAttribute("value"));
    }
    
    [Fact]
    public void ShouldBeEmptyAfterClosing()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var cancelButton = cut.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);

        cancelButton.Click();

        cut.WaitForAssertion(() =>
        {
            fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
            fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
            teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
            Assert.NotNull(teamMemberNameInput);
            Assert.Equal("", teamMemberNameInput.GetAttribute("value"));
        });
        
        positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        positionInputId = positionLabel.Attributes.GetNamedItem("for");
        positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);

        emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        emailInputId = emailLabel.Attributes.GetNamedItem("for");
        emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);

        Assert.Equal("", positionInput.GetAttribute("value"));
        Assert.Equal("", emailInput.GetAttribute("value"));
    }

    [Fact]
    public void ShouldDisplayErrorMessagesIfFieldsAreEmpty()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));
        
        var addMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        addMemberButton.Click();
        
        cut.WaitForAssertion(() => Assert.NotNull(cut.FindElementByCssSelectorAndTextContent(".validation-message", "This field is mandatory")));
        
        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        Assert.Contains("This field is mandatory", fullNameLabel.Parent.ToMarkup());

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        Assert.Contains("This field is mandatory", positionLabel.Parent.ToMarkup());

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        Assert.Contains("This field is mandatory", emailLabel.Parent.ToMarkup());
    }
    
    [Fact]
    public void ShouldRedirectToErrorPageIfThereIsAnErrorCreatingTheMember()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");
        var mockWithCreateTeamMember = new Mock<IWithCreateTeamMember>();
        var mockWithCloseModal = new Mock<IWithCloseModal>();

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);
        mockWithCreateTeamMember.Setup(service => service.CreateTeamMember(It.IsAny<AddTeamMemberDto>()))
            .Throws(new Exception());

        var cut = _ctx.RenderComponent<CreateTeamMemberModal>(
            ComponentParameter.CreateParameter("Show", true),
            ComponentParameter.CreateParameter("TeamId", teamId),
            ComponentParameter.CreateParameter("CreateTeamMember",
                EventCallback.Factory.Create<AddTeamMemberDto>(this,
                    async (AddTeamMemberDto a) => await mockWithCreateTeamMember.Object.CreateTeamMember(a))),
            ComponentParameter.CreateParameter("CloseModal",
                EventCallback.Factory.Create(this, () => mockWithCloseModal.Object.CloseModal())
            ));

        var fullNameLabel = cut.FindElementByCssSelectorAndTextContent("label", "Full name");
        var fullNameInputId = fullNameLabel.Attributes.GetNamedItem("for");
        var teamMemberNameInput = cut.Find($"#{fullNameInputId.TextContent}");
        Assert.NotNull(teamMemberNameInput);
        teamMemberNameInput.Change(teamMember.FullName);

        var positionLabel = cut.FindElementByCssSelectorAndTextContent("label", "Position");
        var positionInputId = positionLabel.Attributes.GetNamedItem("for");
        var positionInput = cut.Find($"#{positionInputId.TextContent}");
        Assert.NotNull(positionInput);
        positionInput.Change(teamMember.Position);

        var emailLabel = cut.FindElementByCssSelectorAndTextContent("label", "Email");
        var emailInputId = emailLabel.Attributes.GetNamedItem("for");
        var emailInput = cut.Find($"#{emailInputId.TextContent}");
        Assert.NotNull(emailInput);
        emailInput.Change(teamMember.Email);

        var addMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addMemberButton);

        addMemberButton.Click();

        cut.WaitForAssertion(() => Assert.Equal("http://localhost/error", _navMan.Uri));
    }
}