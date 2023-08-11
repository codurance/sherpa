using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Dtos.Survey;

namespace BlazorApp.Tests.Services;

public class SurveyServiceTest
{
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly Mock<IHttpClientFactory> _httpClientFactory;

    public SurveyServiceTest()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_handlerMock.Object, false) { BaseAddress = new Uri("http://host") };
        _httpClientFactory = new Mock<IHttpClientFactory>();
        _httpClientFactory.Setup(factory => factory.CreateClient("SherpaBackEnd")).Returns(httpClient);
    }

    [Fact]
    public async Task ShouldReturnEmptyListOfSurveys()
    {
        var surveys = new Survey[] { };
        var surveysJson = await JsonContent.Create(surveys).ReadAsStringAsync();
        
        var responseWithSurveys = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveysJson),
        };
        
        _handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get) && m.RequestUri!.AbsoluteUri.Contains("team") && m.RequestUri!.AbsoluteUri.Contains("surveys")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithSurveys);
        

        ISurveyService SurveyService = new SurveyService(_httpClientFactory.Object);
        
        var actualResponse = await SurveyService.GetAllSurveysByTeam();
        
        var expectedResponse = new Survey[]{ };
        Assert.Equal(JsonConvert.SerializeObject(expectedResponse), JsonConvert.SerializeObject(actualResponse));
    }
}