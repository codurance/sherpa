using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SherpaFrontEnd.Services;
using Xunit.Abstractions;

namespace BlazorApp.Tests.Services;

public class TemplateServiceTest
{

    private ITestOutputHelper output;

    public TemplateServiceTest(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async Task Should_return_templates_returned_by_httpClient()
    {
        
        var handlerMock = new Mock<HttpMessageHandler>();

        var templates = new[] { new TemplateWithNameAndTime("Test", 0) };
        var templatesJson = await JsonContent.Create(templates).ReadAsStringAsync();
        
        output.WriteLine(templatesJson);
        
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
        
        var actualResponse = await templateService.GetAllTemplates();
        
        var expectedResponse = new []{new TemplateWithNameAndTime("Test", 0)};
        Assert.Equal(JsonConvert.SerializeObject(expectedResponse), JsonConvert.SerializeObject(actualResponse));
    }
}