using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SherpaFrontEnd.Services;
using SherpaFrontEnd.Dtos.Survey;
using SherpaFrontEnd.Model;

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
    public async Task ShouldReturnEmptyListOfSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveys = new List<Survey>() { };
        var surveysJson = await JsonContent.Create(surveys).ReadAsStringAsync();
        
        var emptySurveyListResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(surveysJson),
        };
        
        _handlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method.Equals(HttpMethod.Get)  && m.RequestUri!.AbsoluteUri.Contains($"team/{teamId.ToString()}/surveys")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(emptySurveyListResponse);
        

        var surveyService = new SurveyService(_httpClientFactory.Object);
        
        var actualResponse = await surveyService.GetAllSurveysByTeam(teamId);
        
        var expectedResponse = new List<Survey>{ };
        Assert.Equal(JsonConvert.SerializeObject(expectedResponse), JsonConvert.SerializeObject(actualResponse));
    }
    
    [Fact]
    public async Task ShouldReturnExpectedListOfSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var userOne = new User(Guid.NewGuid(), "user");
        var team = new Team(Guid.NewGuid(), "name");
        var surveys = new List<Survey>()
        {
            new Survey(Guid.NewGuid(), userOne, Status.Draft, new DateTime(), "title", "description",
                Array.Empty<Response>(), team, new Template("template"))
        };
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
                    m => m.Method.Equals(HttpMethod.Get)  && m.RequestUri!.AbsoluteUri.Contains($"team/{teamId.ToString()}/surveys")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseWithSurveys);
        

        var surveyService = new SurveyService(_httpClientFactory.Object);
        
        var actualResponse = await surveyService.GetAllSurveysByTeam(teamId);

        Assert.Equal(JsonConvert.SerializeObject(surveys), JsonConvert.SerializeObject(actualResponse));
    }
}