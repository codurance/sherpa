using System.Net;
using System.Net.Http.Json;
using Blazored.Modal;
using Blazored.Modal.Services;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using SherpaFrontEnd;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Acceptance;

public class TemplatesAcceptanceTests
{
    private ITestOutputHelper _outputHelper;
        
    public TemplatesAcceptanceTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async void The_user_can_navigate_to_template_page_and_see_the_hackman_template()
    {
        // GIVEN that an Org coach have a menu on the left
        var ctx = new TestContext();
        ctx.Services.AddBlazoredModal();
        
        var handlerMock = new Mock<HttpMessageHandler>();

        var templates = new[] { new TemplateWithNameAndTime("Hackman Model", 30) };
        var templatesJson = await JsonContent.Create(templates).ReadAsStringAsync();
        
        var responseWithTemplates = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(templatesJson),
        };
        
        handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("template")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithTemplates);
        
        var httpClient = new HttpClient(handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
        ITemplateService templateService = new SherpaFrontEnd.Services.TemplateService(httpClientFactory.Object);
        ctx.Services.AddSingleton<ITemplateService>(templateService);
        
        var nav = ctx.Services.GetRequiredService<FakeNavigationManager>();
        var component = ctx.RenderComponent<App>();

        // WHEN he clicks on Templates
        const string templatesPage = "templates";
        var element = component.Find($"a[href='{templatesPage}']");
        
        Assert.NotNull(element);
        
        // Real navigation does not work
        nav.NavigateTo($"http://localhost/{templatesPage}");

        // THEN he should open Template page with 1 pre-created Hackman template
        var elementWithText = component.FindAll("h2").FirstOrDefault(element => element.InnerHtml.Contains("Hackman Model"));
        
        Assert.NotNull(elementWithText);
    }
    
}