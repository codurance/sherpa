using System.Net;
using System.Net.Http.Json;
using AngleSharp.Dom;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class SurveyAcceptanceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly TemplateWithoutQuestions[] _templates;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly TemplateService _templateService;
    private readonly FakeNavigationManager _navManager;
    private readonly Team[] _teams;
    private readonly TeamServiceHttpClient _teamService;
    private readonly Guid _surveyId = Guid.NewGuid();
    private readonly Mock<IGuidService> _guidService;
    private readonly SurveyService _surveyService;

    public SurveyAcceptanceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithoutQuestions("Hackman Model", 30) };
        _teams = new[] { new Team(Guid.NewGuid(), "Demo Team") };
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _templateService = new TemplateService(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ITemplateService>(_templateService);
        _teamService = new TeamServiceHttpClient(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamService);
        _surveyService = new SurveyService(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(_surveyId);
        _navManager = _testCtx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    private async Task UserShouldBeAbleToNavigateToTemplateDetailsPageWhenClickingOnATemplateInTheTemplatesPage()
    {
        var templatesJson = await JsonContent.Create(_templates).ReadAsStringAsync();
        var responseWithTemplates = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(templatesJson),
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("template")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithTemplates);

        var appComponent = _testCtx.RenderComponent<App>();

        const string targetPage = "templates";
        _navManager.NavigateTo($"http://localhost/{targetPage}");

        var elementBox = appComponent.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        Assert.NotNull(elementBox);

        elementBox.Click();

        appComponent.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/templates/{Uri.EscapeDataString("Hackman Model")}", _navManager.Uri));

        _navManager.NavigateTo($"http://localhost/templates/{Uri.EscapeDataString("Hackman Model")}");

        appComponent.WaitForState(() =>
            appComponent.FindAll("h1")
                .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model")) != null);

        var templateTitle = appComponent.FindAll("h1")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        Assert.NotNull(templateTitle);

        var launchThisTemplateButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Launch this template"));
        Assert.NotNull(launchThisTemplateButton);
    }

    [Fact]
    private async Task UserShouldBeAbleToNavigateToDeliverySettingsPageAndSeeForm()
    {
        // GIVEN that an Org coach is on the Template details page
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"templates/{Uri.EscapeDataString("Hackman Model")}";

        _navManager.NavigateTo($"http://localhost/{targetPage}");

        var teamsJson = await JsonContent.Create(_teams).ReadAsStringAsync();
        var responseWithTeams = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamsJson),
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("team")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithTeams);

        // WHEN he clicks on “Launch this template“
        var launchTemplateButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Launch this template"));
        Assert.NotNull(launchTemplateButton);

        launchTemplateButton.Click();

        // THEN he should be redirected on the Delivery settings page for creating a survey
        // and he should see the following fields:
        // - Select a team - dropdown field - mandatory
        // - title - text field - mandatory
        // - description (then description will be shown to the team members on the page where they will fill in the survey) - text area - not mandatory
        // - Deadline for survey - calendar - not mandator
        // - button Continue

        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}",
                _navManager.Uri));
        
        var teamSelect = appComponent.FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        Assert.NotNull(teamSelect);

        var titleLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Survey title"));

        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);

        var descriptionLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = appComponent.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);

        var deadlineLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("On a specific date"));

        var deadlineInput = appComponent.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(deadlineInput);

        Assert.NotNull(appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue")));
    }
    
    [Fact]
    private async Task UserIsRedirectedToDraftReviewPageAfterCreatingASurvey()
    {
        // GIVEN that an Org coach is on the Delivery settings page for creating a survey
        // and he filled in all fields
        
        var teamsJson = await JsonContent.Create(_teams).ReadAsStringAsync();
        var responseWithTeams = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamsJson),
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("team")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithTeams);
        
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}";

        _navManager.NavigateTo(targetPage);

        
        var surveyCreationResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Post) && m.RequestUri!.AbsoluteUri.Contains("survey")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(surveyCreationResponse);

        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            Guid.NewGuid(), 
            new User(Guid.NewGuid(), "Lucia"), 
            Status.Draft, 
            deadline, 
            "Title", 
            "Description", 
            Array.Empty<Response>(), 
            _teams[0], 
            templateWithoutQuestions);
        
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("survey")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(surveyResponse);
        

        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}",
                _navManager.Uri));
        
        var teamSelect = appComponent.FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        
        Assert.NotNull(teamSelect);
        teamSelect.Change(_teams[0].Id);

        var titleLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Survey title"));

        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);
        titleInput.Change("Title");

        var descriptionLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = appComponent.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);
        descriptionTextArea.Change("Description");

        var deadlineLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("On a specific date"));

        var deadlineInput = appComponent.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(deadlineInput);
        deadlineInput.Change(deadline.Date.ToString("yyyy-MM-dd"));

        
        // WHEN he clicks on Continue

        var continueButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue"));
        Assert.NotNull(continueButton);
        
        continueButton.Click();
        
        _testOutputHelper.WriteLine(appComponent.Markup);
        
        // THEN he should be redirected on the Summary page for a survey
        // and he should see the following info:
        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/survey/draft-review/{_surveyId.ToString()}",
                _navManager.Uri));
        
        // template
        var templateNameElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(templateWithoutQuestions.Name));
        Assert.NotNull(templateNameElement);
        
        // title
        var surveyTitleElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Title));
        Assert.NotNull(surveyTitleElement);
        
        // description
        var surveyDescriptionElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Description));
        Assert.NotNull(surveyDescriptionElement);
        
        // deadline
        var surveyDeadlineElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Deadline.Value.ToString("d-M-yyyy")));
        Assert.NotNull(surveyDeadlineElement);
        
        // name of the team
        var teamNameElement = appComponent.FindAll("p")
            .FirstOrDefault(element => element.InnerHtml.Contains(survey.Team.Name));
        Assert.NotNull(teamNameElement);
        
        // button Back
        var finalBackButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Back"));
        Assert.NotNull(finalBackButton);
        
        // button Launch
        var finalLaunchButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue"));
        Assert.NotNull(finalLaunchButton);
    }

    [Fact]
    private async Task ShouldShowFeedbackIfTryingToCreateASurveyWithoutFillingTheTitleAndTeam()
    {
        // GIVEN that an Org coach is on the Delivery settings page for creating a survey
        var emptyTeamsList = await JsonContent.Create(new List<Team>()).ReadAsStringAsync();
        var responseWithEmptyTeamsList = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(emptyTeamsList),
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("team")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithEmptyTeamsList);
        
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}";

        _navManager.NavigateTo(targetPage);
        
        // WHEN he clicks on Continue
        var continueButton = appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue"));
        Assert.NotNull(continueButton);
        
        continueButton.Click();
        appComponent.WaitForElement(".validation-message");
        
        // and he didn't enter anything to the mandatory fields: Title and Team
        // team
        var teamLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Team"));
        var teamSelector = appComponent.Find($"select#{teamLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(teamSelector);

        var inputTeamGroup = teamSelector.Parent.Parent;
        
        //title
        var titleLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Survey title"));
        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(titleInput);
        
        var inputTitleGroup = titleInput.Parent.Parent;

        // THEN these fields should be highlighted in read and under particular field he should see an error message
        
        Assert.Contains("This field is mandatory", inputTitleGroup.ToMarkup());

        Assert.Contains("This field is mandatory", inputTeamGroup.ToMarkup());

    }
}