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
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Acceptance;

public class SurveyAcceptanceTest
{
    private readonly TestContext _testCtx;
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly TemplateWithNameAndTime[] _templates;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly TemplateService _templateService;
    private readonly FakeNavigationManager _navManager;
    private readonly Team[] _teams;

    public SurveyAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
        _teams = new[] { new Team(Guid.NewGuid(), "Demo Team") };
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _templateService = new TemplateService(_httpClientFactory.Object);
        _testCtx.Services.AddSingleton<ITemplateService>(_templateService);
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
            .SetupSequence<Task<HttpResponseMessage>>(
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
            .SetupSequence<Task<HttpResponseMessage>>(
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
                $"http://localhost/templates/delivery-settings?template={Uri.EscapeDataString("Hackman Model")}",
                _navManager.Uri));
        
        var teamSelect = appComponent.FindAll("select")
            .FirstOrDefault(element => element.InnerHtml.Contains("Demo Team"));
        Assert.NotNull(teamSelect);
        Assert.True(teamSelect.IsRequired());

        var titleLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Title"));

        var titleInput = appComponent.Find($"input#{titleLabel!.Attributes.GetNamedItem("for")}");

        Assert.NotNull(titleInput);
        Assert.True(titleInput.IsRequired());

        var descriptionLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Description"));

        var descriptionTextArea = appComponent.Find($"textarea#{descriptionLabel!.Attributes.GetNamedItem("for")}");
        Assert.NotNull(descriptionTextArea);
        Assert.False(descriptionTextArea.IsRequired());

        var deadlineLabel = appComponent.FindAll("label")
            .FirstOrDefault(element => element.InnerHtml.Contains("Deadline"));

        var deadlineInput = appComponent.Find($"input#{deadlineLabel!.Attributes.GetNamedItem("for")}");

        Assert.NotNull(deadlineInput);
        Assert.False(deadlineInput.IsRequired());

        Assert.NotNull(appComponent.FindAll("button")
            .FirstOrDefault(element => element.InnerHtml.Contains("Continue")));
    }
}