using System.Globalization;
using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class DeliverySettingsTest
{
    private TestContext _ctx;
    private Mock<ITeamDataService> _teamService;
    private FakeNavigationManager _navMan;
    private readonly List<Team>? _teams;
    private readonly Mock<IGuidService> _guidService;
    private Guid _surveyId = Guid.NewGuid();
    private readonly Mock<ISurveyService> _surveyService;


    public DeliverySettingsTest()
    {
        _ctx = new TestContext();
        _teamService = new Mock<ITeamDataService>();
        _ctx.Services.AddSingleton<ITeamDataService>(_teamService.Object);
        _surveyService = new Mock<ISurveyService>();
        _ctx.Services.AddSingleton<ISurveyService>(_surveyService.Object);
        _guidService = new Mock<IGuidService>();
        _ctx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(_surveyId);
        _navMan = _ctx.Services.GetRequiredService<FakeNavigationManager>();
        _teams = new List<Team>() { new Team(Guid.NewGuid(), "Demo Team") };
    }

    [Fact]
    public async Task ShouldRenderSettingsForm()
    {
        _teamService.Setup(service => service.GetAllTeams()).ReturnsAsync(_teams);

        var component =
            _ctx.RenderComponent<DeliverySettings>(ComponentParameter.CreateParameter("Template", "Hackman Model"));

        var teamSelect = component
            .FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        Assert.NotNull(teamSelect);

        var titleLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Survey title"));

        var titleInput = component.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);

        var descriptionLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = component.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);

        var deadlineLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("On a specific date"));

        var deadlineInput = component.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(deadlineInput);

        Assert.NotNull(component.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue")));
    }

    [Fact]
    public async Task ShouldCallCreateSurveyWithInfoFromTheForm()
    {
        _teamService.Setup(service => service.GetAllTeams()).ReturnsAsync(_teams);

        const string templateName = "Hackman Model";
        var component =
            _ctx.RenderComponent<DeliverySettings>(ComponentParameter.CreateParameter("Template", templateName));

        var teamSelect = component
            .FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));

        Assert.NotNull(teamSelect);
        teamSelect.Change(_teams[0].Id);

        const string surveyTitle = "Survey title";
        var titleLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains(surveyTitle));

        var titleInput = component.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);
        titleInput.Change(surveyTitle);

        const string surveyDescription = "Description";
        var descriptionLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains(surveyDescription));

        var descriptionTextArea = component.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);
        descriptionTextArea.Change(surveyDescription);

        var deadlineLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("On a specific date"));

        var deadlineInput = component.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");
        var deadline = DateTime.Parse("03/12/2020");
        ;
        deadlineInput.Change(deadline.Date.ToString("yyyy-M-d"));

        Assert.NotNull(deadlineInput);

        // WHEN he clicks on Continue

        var continueButton = component.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue"));
        Assert.NotNull(continueButton);

        continueButton.Click();

        var invocation = _surveyService.Invocations[0];

        Assert.Equal("CreateSurvey", invocation.Method.Name);
        CustomAssertions.StringifyEquals(
            new CreateSurveyDto(_surveyId, _teams[0].Id, templateName, surveyTitle, surveyDescription, deadline),
            invocation.Arguments[0]);
    }

    [Fact]
    public async Task ShouldRedirectToDraftReviewPageAfterClickingContinue()
    {
        _teamService.Setup(service => service.GetAllTeams()).ReturnsAsync(_teams);

        const string templateName = "Hackman Model";
        var component =
            _ctx.RenderComponent<DeliverySettings>(ComponentParameter.CreateParameter("Template", templateName));

        var teamSelect = component
            .FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));

        Assert.NotNull(teamSelect);
        teamSelect.Change(_teams[0].Id);

        const string surveyTitle = "Survey title";
        var titleLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains(surveyTitle));

        var titleInput = component.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);
        titleInput.Change(surveyTitle);

        const string surveyDescription = "Description";
        var descriptionLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains(surveyDescription));

        var descriptionTextArea = component.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);
        descriptionTextArea.Change(surveyDescription);

        var deadlineLabel = component.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("On a specific date"));

        var deadlineInput = component.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");
        var deadline = DateTime.Parse("03/12/2020");
        
        deadlineInput.Change(deadline.Date.ToString("yyyy-M-d"));

        Assert.NotNull(deadlineInput);

        // WHEN he clicks on Continue

        var continueButton = component.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue"));
        Assert.NotNull(continueButton);

        continueButton.Click();

        component.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/survey/draft-review/{_surveyId.ToString()}", _navMan.Uri));
    }
}