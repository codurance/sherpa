using System.Net;
using System.Net.Http.Json;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
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

    public SurveyAcceptanceTest()
    {
        _testCtx = new TestContext();
        _testCtx.Services.AddBlazoredModal();
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
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

        var targetPage = "templates";
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
}