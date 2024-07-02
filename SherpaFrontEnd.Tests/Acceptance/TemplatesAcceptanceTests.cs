using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using Blazored.Modal;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Acceptance;

public class TemplatesAcceptanceTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock;

    private readonly TemplateWithoutQuestions[] _templates;


    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private readonly Mock<IAuthService> _authService;

    private readonly ITemplateService _templateService;
    private readonly TestContext _ctx;
    private readonly FakeNavigationManager _nav;

    public TemplatesAcceptanceTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        _templates = new[] { new TemplateWithoutQuestions("Hackman Model", 30) };
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        _authService = new Mock<IAuthService>();
        _authService.Setup(auth => auth.DecorateWithToken(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync((HttpRequestMessage requestMessage) => requestMessage);
        _templateService = new TemplateService(_httpClientFactory.Object, _authService.Object);
        _ctx = new TestContext();
        _ctx.Services.AddBlazoredModal();
        _ctx.Services.AddBlazoredLocalStorage();
        _ctx.Services.AddScoped<ICookiesService, CookiesService>();
        _ctx.JSInterop.Setup<string>("localStorage.getItem", "CookiesAcceptedDate");
        _ctx.Services.AddSingleton<ITemplateService>(_templateService);
        var auth = _ctx.AddTestAuthorization();
        auth.SetAuthorized("Demo user");
        auth.SetClaims(new []{new Claim("username", "Demo user")});
        _nav = _ctx.Services.GetRequiredService<FakeNavigationManager>();
    }

    [Fact]
    public async Task The_user_can_navigate_to_template_page_and_see_the_hackman_template()
    {
        // GIVEN that an Org coach have a menu on the left
        var templatesJson = await JsonContent.Create(_templates).ReadAsStringAsync();
        var responseWithTemplates = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(templatesJson),
        };
        
        _handlerMock.SetupRequest(HttpMethod.Get, "template", responseWithTemplates);
        

        var component = _ctx.RenderComponent<App>();

        // WHEN he clicks on Templates
        const string templatesPage = "templates";
        var element = component.Find($"a[href='{templatesPage}']");

        Assert.NotNull(element);

        // Real navigation does not work
        _nav.NavigateTo($"http://localhost/{templatesPage}");

        // THEN he should open Template page with 1 pre-created Hackman template
        var elementWithText = component.FindElementByCssSelectorAndTextContent("h2", "Hackman Model");

        Assert.NotNull(elementWithText);
    }

    [Fact]
    public async Task The_user_will_see_an_error_message_if_there_is_an_error_fetching_the_templates()
    {
        // GIVEN that an Org coach have a menu on the left
        
        _handlerMock.SetupRequestWithException(HttpMethod.Get, "template", new Exception());

        var component = _ctx.RenderComponent<App>();

        // WHEN he clicks on Templates but an error happens
        const string templatesPage = "templates";
        var element = component.Find($"a[href='{templatesPage}']");

        Assert.NotNull(element);

        // Real navigation does not work
        _nav.NavigateTo($"http://localhost/{templatesPage}");

        // THEN he should open Template page with an error message
        var elementWithText = component.FindElementByCssSelectorAndTextContent("p", "There has been an error loading the templates");

        Assert.NotNull(elementWithText);
    }
}