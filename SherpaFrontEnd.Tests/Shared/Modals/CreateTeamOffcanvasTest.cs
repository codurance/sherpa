using AngleSharp.Common;
using BlazorApp.Tests.Acceptance;

using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Shared.Modals;

namespace BlazorApp.Tests.Shared.Modals;

public class CreateTeamOffcanvasTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<IGuidService> _guidService;
    private readonly Mock<ITeamDataService> _teamService;
    private readonly FakeNavigationManager _navMan;
    private readonly Guid _teamId;
    private readonly Mock<IToastNotificationService> _toastService;

    public CreateTeamOffcanvasTest()
    {
        _testCtx = new TestContext();
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        
        _teamId = Guid.NewGuid();
        _guidService.Setup(s => s.GenerateRandomGuid()).Returns(_teamId);
        
        _teamService = new Mock<ITeamDataService>();
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamService.Object);
        
        _toastService = new Mock<IToastNotificationService>();
        _testCtx.Services.AddSingleton<IToastNotificationService>(_toastService.Object);

        _navMan = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }
    

    [Fact]
    public void ShouldRenderAllInputsAndButtons()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var createNewTeamTitle = component.FindElementByCssSelectorAndTextContent("h1,h2,h3", "Create new team");
        Assert.NotNull(createNewTeamTitle);
        
        var teamNameLabel = component.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.FindAll($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);
        
        var continueButton = component.FindElementByCssSelectorAndTextContent("button", "Confirm");
        var cancelButton = component.FindElementByCssSelectorAndTextContent("button", "Cancel");
        
        Assert.NotNull(continueButton);
        Assert.NotNull(cancelButton);
    }

    [Fact]
    public async Task ShouldCallServiceAndRedirectToCreatedTeamPage()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var teamNameLabel = component.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        const string teamName = "Demo team";
        teamNameInput.Change(teamName);
        
        var confirmButton = component.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();
        
        _teamService.Verify(service => service.AddTeam(It.IsAny<Team>()));
        Assert.Equal($"http://localhost/team-content/{_teamId.ToString()}", _navMan.Uri);
    }

    [Fact]
    public void ShouldShowSuccessToastNotificationWhenCreatedTeam()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var teamNameLabel = component.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        const string teamName = "Demo team";
        teamNameInput.Change(teamName);
        
        var confirmButton = component.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();
        
        _toastService.Verify(service => service.ShowSuccess("Team created successfully"));
    }
    
    [Fact]
    public async Task ShouldCallJsInteropWithCloseOffcanvasIfCancelIsClicked()
    {
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        _testCtx.JSInterop.SetupVoid("hideOffCanvas", "create-new-team-form").SetVoidResult();
        
        var cancelButton
            = component.FindElementByCssSelectorAndTextContent("button", "Cancel");
        Assert.NotNull(cancelButton);
        
        cancelButton.Click();
        
        var jsRuntimeInvocation = _testCtx.JSInterop.Invocations.GetItemByIndex(0);

        Assert.Equal("hideOffCanvas", jsRuntimeInvocation.Identifier);
        Assert.Contains("create-new-team-form", jsRuntimeInvocation.Arguments);
    }

    [Fact]
    public async Task ShouldRedirectToErrorPageIfFoundErrorWhenAddingTeam()
    {
        _teamService.Setup(_ => _.AddTeam(It.IsAny<Team>())).ThrowsAsync(new Exception());
        
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var teamNameLabel = component.FindElementByCssSelectorAndTextContent("label", "Team name");
        var teamNameInputId = teamNameLabel.Attributes.GetNamedItem("for");
        var teamNameInput = component.Find($"#{teamNameInputId.TextContent}");
        Assert.NotNull(teamNameInput);

        const string teamName = "Demo team";
        teamNameInput.Change(teamName);
        
        var confirmButton = component.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();
        
        Assert.Equal($"http://localhost/error", _navMan.Uri);
    }
    
    [Fact]
    public async Task ShouldShowMandatoryFieldErrorFeedbackIfUserDoesNotFillItBeforeClickingContinue()
    {
        _teamService.Setup(_ => _.AddTeam(It.IsAny<Team>())).ThrowsAsync(new Exception());
        
        var component = _testCtx.RenderComponent<CreateTeamOffcanvas>();
        
        var confirmButton = component.FindElementByCssSelectorAndTextContent("button", "Confirm");
        Assert.NotNull(confirmButton);
        
        confirmButton.Click();

        component.WaitForElement(".validation-message");
        
        var teamNameLabelAfterClick = component.FindElementByCssSelectorAndTextContent("label", "Team name");
        var inputGroup = teamNameLabelAfterClick.Parent;
        
        Assert.Contains("This field is mandatory", inputGroup.ToMarkup());
    }
}