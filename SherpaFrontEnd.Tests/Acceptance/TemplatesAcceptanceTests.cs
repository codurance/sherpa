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

public class TemplatesAcceptanceTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock;

    private readonly TemplateWithNameAndTime[] _templates;


    private readonly Mock<IHttpClientFactory> _httpClientFactory;

    private readonly ITemplateService _templateService;

    public TemplatesAcceptanceTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _templateService = new TemplateService(_httpClientFactory.Object);

    }

    [Fact]
    public async Task The_user_can_navigate_to_template_page_and_see_the_hackman_template()
    {
        // GIVEN that an Org coach have a menu on the left
        var ctx = new TestContext();
        ctx.Services.AddBlazoredModal();

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
        
        
        ctx.Services.AddSingleton<ITemplateService>(_templateService);

        var nav = ctx.Services.GetRequiredService<FakeNavigationManager>();
        var component = ctx.RenderComponent<App>();

        // WHEN he clicks on Templates
        const string templatesPage = "templates";
        var element = component.Find($"a[href='{templatesPage}']");

        Assert.NotNull(element);

        // Real navigation does not work
        nav.NavigateTo($"http://localhost/{templatesPage}");

        // THEN he should open Template page with 1 pre-created Hackman template
        var elementWithText = component.FindAll("h2")
            .FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));

        Assert.NotNull(elementWithText);
    }

    [Fact]
    public async Task The_user_will_see_an_error_message_if_there_is_an_error_fetching_the_templates()
    {
        // GIVEN that an Org coach have a menu on the left
        var ctx = new TestContext();
        ctx.Services.AddBlazoredModal();

        var templatesJson = await JsonContent.Create(_templates).ReadAsStringAsync();
        
        _handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("template")),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception());

        
        ITemplateService templateService = new TemplateService(_httpClientFactory.Object);
        ctx.Services.AddSingleton<ITemplateService>(templateService);

        var nav = ctx.Services.GetRequiredService<FakeNavigationManager>();
        var component = ctx.RenderComponent<App>();

        // WHEN he clicks on Templates but an error happens
        const string templatesPage = "templates";
        var element = component.Find($"a[href='{templatesPage}']");

        Assert.NotNull(element);

        // Real navigation does not work
        nav.NavigateTo($"http://localhost/{templatesPage}");

        // THEN he should open Template page with an error message
        var elementWithText = component.FindAll("p").FirstOrDefault(element =>
            element.InnerHtml.Contains("There has been an error loading the templates"));

        Assert.NotNull(elementWithText);
    }
}