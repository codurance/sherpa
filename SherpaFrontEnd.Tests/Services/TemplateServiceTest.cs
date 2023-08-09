using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SherpaBackEnd.Tests.Helpers;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Services;

public class TemplateServiceTest
{
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;

    public TemplateServiceTest()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
    }

    [Fact]
    public async Task Should_return_templates_returned_by_httpClient()
    {
        var templates = new[] { new TemplateWithNameAndTime("Test", 0) };
        var templatesJson = await JsonContent.Create(templates).ReadAsStringAsync();
        
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
        

        ITemplateService templateService = new TemplateService(_httpClientFactory.Object);
        
        var actualResponse = await templateService.GetAllTemplates();
        
        var expectedResponse = new []{new TemplateWithNameAndTime("Test", 0)};
        CustomAssertions.StringifyEquals(expectedResponse, actualResponse);
    }
}