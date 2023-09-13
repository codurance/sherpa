using AngleSharp.Dom;

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Test.Helpers;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Pages;

public class TeamContentTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private TestContext _testContext;
    private IRenderedComponent<TeamContent> _renderedComponent;
    private Mock<ITeamDataService> _mockTeamService;
    private Mock<ISurveyService> _mockSurveyService;
    private readonly Mock<IGuidService> _mockGuidService;

    public TeamContentTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testContext = new TestContext();
        _mockTeamService = new Mock<ITeamDataService>();
        _mockSurveyService = new Mock<ISurveyService>();
        _mockGuidService = new Mock<IGuidService>();
        _testContext.Services.AddScoped(s => _mockTeamService.Object);
        _testContext.Services.AddSingleton<ISurveyService>(_mockSurveyService.Object);
        _testContext.Services.AddSingleton<IGuidService>(_mockGuidService.Object);
    }

    [Fact]
    public void SingleTeamIsRendered()
    {
        const string teamName = "Demo team";
        var team = new Team
        {
            Name = teamName,
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        _mockSurveyService.Setup(m => m.GetAllSurveysByTeam(It.IsAny<Guid>())).ReturnsAsync(new List<Survey>());


        var teamDetailsPage = _testContext.RenderComponent<TeamContent>();
        teamDetailsPage.WaitForAssertion(() =>
        {
            Assert.NotNull(teamDetailsPage.FindElementByCssSelectorAndTextContent("h3", teamName));
        });
        var analysisTab = teamDetailsPage.FindElementByCssSelectorAndTextContent("li", "Analysis");
        var sendNewSurveyTeam = teamDetailsPage.FindElementByCssSelectorAndTextContent("button", "Send a new survey");

        Assert.NotNull(analysisTab);
        Assert.NotNull(sendNewSurveyTeam);
    }

    [Fact]
    public void ShouldDisplayTeamSurveysTabPage()
    {
        const string teamName = "Demo team";
        var team = new Team
        {
            Name = teamName,
            Id = Guid.NewGuid()
        };

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        _mockSurveyService.Setup(m => m.GetAllSurveysByTeam(It.IsAny<Guid>())).ReturnsAsync(new List<Survey>());
        var teamContentComponent = _testContext.RenderComponent<TeamContent>();

        teamContentComponent.WaitForAssertion(() =>
            Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys")));
    }

    [Fact]
    public void ShouldDisplayTeamSurveysTabPageContentWhenEmptySurveysList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };

        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>());

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent =
            _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));

        var surveyTabPage = teamContentComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys");
        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();


        teamContentComponent.WaitForAssertion(() =>
            Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("button", "Send first survey")));
        Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("p",
            "Let's begin the journey towards a stronger, more effective team!"));
        Assert.NotNull(
            teamContentComponent.FindElementByCssSelectorAndTextContent("p", "You don’t have any surveys yet"));
    }

    [Fact]
    public void ShouldDisplayTeamSurveysTabPageContentWhenNonEmptyList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };
        var userOne = new User(Guid.NewGuid(), "user");

        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>()
        {
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
                Array.Empty<Response>(), team, new Template("template"))
        });

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent =
            _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));

        var surveyTabPage = teamContentComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys");

        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();

        teamContentComponent.WaitForAssertion(() =>
            Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("h3", "All Surveys")));
        
        Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("button", "Send new survey"));
    }

    [Fact]
    public void ShouldDisplayCorrectHeadingsForUserTableInTeamSurveysTabPageContentWhenNonEmptyList()
    {
        const string teamName = "Demo team";
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Name = teamName,
            Id = teamId
        };
        var userOne = new User(Guid.NewGuid(), "user");

        _mockSurveyService.Setup(_ => _.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>()
        {
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
                Array.Empty<Response>(), team, new Template("template"))
        });

        _mockTeamService.Setup(m => m.GetTeamById(It.IsAny<Guid>())).ReturnsAsync(team);
        var teamContentComponent =
            _testContext.RenderComponent<TeamContent>(ComponentParameter.CreateParameter("TeamId", teamId));
        
        var surveyTabPage = teamContentComponent.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Surveys");
        Assert.NotNull(surveyTabPage);
        surveyTabPage.Click();

        
        var surveyName = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Survey title");
        var template = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Template");
        var coach = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Coach");
        var deadline = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Deadline");
        var participants = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Participants");
        var status = teamContentComponent.FindElementByCssSelectorAndTextContent("th", "Status");

        teamContentComponent.WaitForAssertion(() =>
            Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("h3", "All Surveys")));
        Assert.NotNull(teamContentComponent.FindElementByCssSelectorAndTextContent("button", "Send new survey"));
        Assert.NotNull(surveyName);
        Assert.NotNull(template);
        Assert.NotNull(coach);
        Assert.NotNull(deadline);
        Assert.NotNull(participants);
        Assert.NotNull(status);
    }

    [Fact]
    public void ShouldDisplayCorrectSurveyDataInUserTable()
    {
        var userOne = new User(Guid.NewGuid(), "testUser");
        const string newTeamName = "Team with survey";
        var testTeamId = Guid.NewGuid();
        var testTeam = new Team(testTeamId, newTeamName);

        var testSurvey = new List<Survey>
        {
            new Survey(
                Guid.NewGuid(),
                userOne,
                Status.Draft,
                new DateTime(),
                "title",
                "description",
                Array.Empty<Response>(),
                testTeam,
                new Template("template")
            )
        };

        var surveyTableComponent =
            _testContext.RenderComponent<SurveyTable>(ComponentParameter.CreateParameter("Surveys", testSurvey));

        var rows = surveyTableComponent.FindAll("tr.bg-white");
        Assert.Equal(testSurvey.Count, rows.Count);

        foreach (var survey in testSurvey)
        {
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Title), null));
            Assert.NotNull(rows.FirstOrDefault(
                element => element.ToMarkup().Contains(survey.Deadline.Value.Date.ToString("dd/MM/yyyy")), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Template.Name), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(survey.Coach.Name), null));
            Assert.NotNull(rows.FirstOrDefault(element => element.ToMarkup().Contains(Status.Draft.ToString()), null));
        }
    }

    [Fact]
    public void ShouldCallAddTeamMemberOnTeamServiceWhenFillingTheAddTeamMemberModal()
    {
        var teamId = Guid.NewGuid();
        var teamMember = new TeamMember(Guid.NewGuid(), "Full name", "Some position", "demo@demo.com");

        _mockGuidService.Setup(service => service.GenerateRandomGuid()).Returns(teamMember.Id);
        _mockTeamService.Setup(service => service.GetTeamById(teamId)).ReturnsAsync(new Team(teamId, "Team name"));
        _mockSurveyService.Setup(service => service.GetAllSurveysByTeam(teamId)).ReturnsAsync(new List<Survey>());

        var cut = _testContext.RenderComponent<TeamContent>(
            ComponentParameter.CreateParameter("TeamId", teamId)
        );

        var membersTab = cut.FindElementByCssSelectorAndTextContent("a:not(a[href])", "Members");

        Assert.NotNull(membersTab);
        membersTab.Click();

        cut.WaitForAssertion(() =>
            Assert.NotNull(cut.FindElementByCssSelectorAndTextContent("button", "Add member")));

        var addTeamMemberButton = cut.FindElementByCssSelectorAndTextContent("button", "Add member");
        Assert.NotNull(addTeamMemberButton);

        addTeamMemberButton.Click();

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

        var mockMethodInvocations = _mockTeamService.Invocations;

        Assert.Equal("GetTeamById", mockMethodInvocations[0].Method.Name);
        CustomAssertions.StringifyEquals(teamId, mockMethodInvocations[0].Arguments[0]);

        Assert.Equal("AddTeamMember", mockMethodInvocations[1].Method.Name);
        CustomAssertions.StringifyEquals(new AddTeamMemberDto(teamId, teamMember),
            mockMethodInvocations[1].Arguments[0]);

        Assert.Equal("GetTeamById", mockMethodInvocations[2].Method.Name);
        CustomAssertions.StringifyEquals(teamId, mockMethodInvocations[2].Arguments[0]);
    }
}