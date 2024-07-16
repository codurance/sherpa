using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Pages.DeliverySettings;
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
    private Mock<IAuthService> _authService;

    public SurveyAcceptanceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _testCtx.Services.AddBlazoredLocalStorage();
        _testCtx.Services.AddBlazoredToast();
        _testCtx.Services.AddScoped<ICookiesService, CookiesService>();
        _testCtx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithoutQuestions("Hackman Model", 30) };
        _teams = new[] { new Team(Guid.NewGuid(), "Demo Team") };
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _templateService = new TemplateService(_httpClientFactory.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ITemplateService>(_templateService);
        _teamService = new TeamServiceHttpClient(_httpClientFactory.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ITeamDataService>(_teamService);
        _surveyService = new SurveyService(_httpClientFactory.Object, _authService.Object);
        _testCtx.Services.AddSingleton<ISurveyService>(_surveyService);
        _guidService = new Mock<IGuidService>();
        _testCtx.Services.AddSingleton<IGuidService>(_guidService.Object);
        _testCtx.Services.AddScoped<IToastNotificationService, BlazoredToastService>();
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(_surveyId);
        var auth = _testCtx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new[] { new Claim("username", "Demo user") });
        _testCtx.Services.AddSingleton<IAnalysisService>(new Mock<IAnalysisService>().Object);
        _testCtx.JSInterop.SetupVoid("generateColumnsChart", _ => true);
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

        _handlerMock.SetupRequest(HttpMethod.Get, "template", responseWithTemplates);

        var appComponent = _testCtx.RenderComponent<App>();

        const string targetPage = "templates";
        _navManager.NavigateTo($"http://localhost/{targetPage}");

        var elementBox = appComponent.FindElementByCssSelectorAndTextContent("h2", "Hackman Model");
        Assert.NotNull(elementBox);

        elementBox.Click();

        appComponent.WaitForAssertion(() =>
            Assert.Equal($"http://localhost/templates/{Uri.EscapeDataString("Hackman Model")}", _navManager.Uri));

        _navManager.NavigateTo($"http://localhost/templates/{Uri.EscapeDataString("Hackman Model")}");

        appComponent.WaitForState(() =>
            appComponent.FindElementByCssSelectorAndTextContent("h1", "Hackman Model") != null);

        var templateTitle = appComponent.FindElementByCssSelectorAndTextContent("h1", "Hackman Model");
        Assert.NotNull(templateTitle);

        var launchThisTemplateButton =
            appComponent.FindElementByCssSelectorAndTextContent("button", "Launch this template");
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

        _handlerMock.SetupRequest(HttpMethod.Get, "team", responseWithTeams);

        // WHEN he clicks on “Launch this template“
        var launchTemplateButton =
            appComponent.FindElementByCssSelectorAndTextContent("button", "Launch this template");
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

        var teamSelect = appComponent.FindElementByCssSelectorAndTextContent("select", "Demo Team");
        Assert.NotNull(teamSelect);

        var titleLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Survey title");

        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);

        var descriptionLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Description");

        var descriptionTextArea =
            appComponent.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);

        var deadlineLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "On a specific date");

        var deadlineInput = appComponent.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(deadlineInput);

        Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("button", "Continue"));
    }

    [Fact]
    private async Task UserIsRedirectedToDraftReviewPageAfterCreatingASurvey()
    {
        // GIVEN that an Org coach is on the Delivery settings page for creating a survey
        // And they filled in all fields
        // And they use the default description
        _guidService.Setup(service => service.GenerateRandomGuid()).Returns(_surveyId);
        var teamsJson = await JsonContent.Create(_teams).ReadAsStringAsync();
        var responseWithTeams = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamsJson),
        };

        _handlerMock.SetupRequest(HttpMethod.Get, "team", responseWithTeams);

        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}";

        _navManager.NavigateTo(targetPage);


        var surveyCreationResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
        };

        _handlerMock.SetupRequest(HttpMethod.Post, "survey", surveyCreationResponse);

        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId,
            new User(Guid.NewGuid(), "Lucia"),
            Status.Draft,
            deadline,
            "Title",
            SurveyCopy.DefaultDescription(),
            Array.Empty<Response>(),
            _teams[0],
            templateWithoutQuestions);

        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };

        _handlerMock.SetupRequest(HttpMethod.Get, $"survey/{_surveyId}", surveyResponse);


        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}",
                _navManager.Uri));

        var teamSelect = appComponent.FindElementByCssSelectorAndTextContent("select", "Demo Team");

        Assert.NotNull(teamSelect);
        teamSelect.Change(_teams[0].Id);

        var titleLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Survey title");

        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");

        Assert.NotNull(titleInput);
        titleInput.Change("Title");

        var descriptionLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Description");

        var descriptionTextArea =
            appComponent.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(descriptionTextArea);
        Assert.Equal(survey.Description, descriptionTextArea.GetAttribute("value"));

        var deadlineLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "On a specific date");

        var deadlineInput = appComponent.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(deadlineInput);
        deadlineInput.Change(deadline.Date.ToString("yyyy-MM-dd"));


        // WHEN he clicks on Continue

        var continueButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Continue");
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
        var templateNameElement =
            appComponent.FindElementByCssSelectorAndTextContent("p", templateWithoutQuestions.Name);
        Assert.NotNull(templateNameElement);

        // title
        var surveyTitleElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Title);
        Assert.NotNull(surveyTitleElement);

        // description
        var surveyDescriptionElement =
            appComponent.Find("p[data-testid='survey-description']");
        Assert.NotNull(surveyDescriptionElement);
        // deadline
        var surveyDeadlineElement =
            appComponent.FindElementByCssSelectorAndTextContent("li", survey.Deadline.Value.ToString("dd/MM/yyyy"));
        Assert.NotNull(surveyDeadlineElement);

        // name of the team
        var teamNameElement = appComponent.FindElementByCssSelectorAndTextContent("p", survey.Team.Name);
        Assert.NotNull(teamNameElement);

        // button Launch
        var finalLaunchButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
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


        _handlerMock.SetupRequest(HttpMethod.Get, "team", responseWithEmptyTeamsList);

        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}";

        _navManager.NavigateTo(targetPage);

        // WHEN he clicks on Continue
        var continueButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Continue");
        Assert.NotNull(continueButton);

        continueButton.Click();
        appComponent.WaitForElement(".validation-message");

        // and he didn't enter anything to the mandatory fields: Title and Team
        // team
        var teamLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Team");
        var teamSelector = appComponent.Find($"select#{teamLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(teamSelector);

        var inputTeamGroup = teamSelector.Parent.Parent;

        //title
        var titleLabel = appComponent.FindElementByCssSelectorAndTextContent("label", "Survey title");
        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for").Value}");
        Assert.NotNull(titleInput);

        var inputTitleGroup = titleInput.Parent.Parent;

        // THEN these fields should be highlighted in read and under particular field he should see an error message

        Assert.Contains("This field is mandatory", inputTitleGroup.ToMarkup());

        Assert.Contains("This field is mandatory", inputTeamGroup.ToMarkup());
    }

    [Fact]
    public async Task ShouldBeRedirectedToTeamPageWhenLaunchingSurveyAndWasSuccessful()
    {
        // GIVEN that an Org coach is on the Review survey page after creating a survey
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId, 
            new User(Guid.NewGuid(), "Lucia"), 
            Status.Draft, 
            deadline, 
            "Survey Test",
            SurveyCopy.DefaultDescription(), 
            Array.Empty<Response>(), 
            _teams[0], 
            templateWithoutQuestions);
        
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"survey/{_surveyId}", surveyResponse);
        
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/draft-review/{_surveyId}";

        _navManager.NavigateTo(targetPage);
        
        // WHEN he clicks on Launch survey
        // AND it is launched successfully
        var launchResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
        };

        var teamJson = await JsonContent.Create(survey.Team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };

        var teamSurveysJson = await JsonContent.Create(new List<Survey>()).ReadAsStringAsync();
        var teamSurveysResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamSurveysJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Post, "survey-notifications", launchResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}", teamResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}/surveys", teamSurveysResponse);
        
        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        Assert.NotNull(launchSurveyButton);
        
        launchSurveyButton.Click();
        
        // THEN he should be redirected on the Team page, Surveys tab
        // AND he should see the following info:
        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/team-content/{_teams[0].Id.ToString()}/surveys",
                _navManager.Uri));
        
        // team title
        appComponent.FindElementByCssSelectorAndTextContent("h3", survey.Team.Name);
    }

    [Fact]
    public async Task ShouldShowSuccessToastNotificationWhenLaunchingSurveyAndWasSuccessful()
    {
        // GIVEN that an Org coach is on the Review survey page after creating a survey
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId, 
            new User(Guid.NewGuid(), "Lucia"), 
            Status.Draft, 
            deadline, 
            "Survey Test",
            SurveyCopy.DefaultDescription(), 
            Array.Empty<Response>(), 
            _teams[0], 
            templateWithoutQuestions);
        
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"survey/{_surveyId}", surveyResponse);
        
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/draft-review/{_surveyId}";

        _navManager.NavigateTo(targetPage);
        
        // WHEN he clicks on Launch survey
        // AND it is launched successfully
        var launchResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
        };

        var teamJson = await JsonContent.Create(survey.Team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };

        var teamSurveysJson = await JsonContent.Create(new List<Survey>()).ReadAsStringAsync();
        var teamSurveysResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamSurveysJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Post, "survey-notifications", launchResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}", teamResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}/surveys", teamSurveysResponse);
        
        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        Assert.NotNull(launchSurveyButton);
        
        launchSurveyButton.Click();
        
        // THEN he should see a toast message with the message "Survey launched successfully"
        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "Survey launched successfully")));
    }

    [Fact]
    public async Task ShouldBeRedirectedTeamPageAndShowErrorToastNotificationWhenLaunchingSurveyAndWasUnsuccessful()
    {
        // GIVEN that an Org coach is on the Review survey page after creating a survey
        var deadline = DateTime.Now;
        var templateWithoutQuestions = new TemplateWithoutQuestions("Hackman Model", 30);
        var survey = new SurveyWithoutQuestions(
            _surveyId, 
            new User(Guid.NewGuid(), "Lucia"), 
            Status.Draft, 
            deadline, 
            "Survey Test",
            SurveyCopy.DefaultDescription(), 
            Array.Empty<Response>(), 
            _teams[0], 
            templateWithoutQuestions);
        
        var surveyJson = await JsonContent.Create(survey).ReadAsStringAsync();
        var surveyResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveyJson)
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, $"survey/{_surveyId}", surveyResponse);
        
        var appComponent = _testCtx.RenderComponent<App>();

        var targetPage = $"http://localhost/survey/draft-review/{_surveyId}";

        _navManager.NavigateTo(targetPage);
        
        var teamJson = await JsonContent.Create(survey.Team).ReadAsStringAsync();
        var teamResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamJson)
        };

        var teamSurveysJson = await JsonContent.Create(new List<Survey>()).ReadAsStringAsync();
        var teamSurveysResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(teamSurveysJson)
        };
        
        var launchResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError,
        };
        _handlerMock.SetupRequest(HttpMethod.Post, "survey-notifications", launchResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}", teamResponse);
        _handlerMock.SetupRequest(HttpMethod.Get, $"/team/{survey.Team.Id}/surveys", teamSurveysResponse);
        
        // WHEN he clicks on Launch survey
        // AND it is launched unsuccessfully
        
        var launchSurveyButton = appComponent.FindElementByCssSelectorAndTextContent("button", "Launch survey");
        Assert.NotNull(launchSurveyButton);
        
        launchSurveyButton.Click();
        
        // THEN he should be redirected on the Team page, Surveys tab
        // AND he should see the following info:
        appComponent.WaitForAssertion(() =>
            Assert.Equal(
                $"http://localhost/team-content/{_teams[0].Id.ToString()}/surveys",
                _navManager.Uri));
        
        // team title
        appComponent.FindElementByCssSelectorAndTextContent("h3", survey.Team.Name);
        
        // AND he should see an error toast notification
        appComponent.WaitForAssertion(() => Assert.NotNull(appComponent.FindElementByCssSelectorAndTextContent("p", "The survey wasn't launched successfully, please try again")));
    }
}