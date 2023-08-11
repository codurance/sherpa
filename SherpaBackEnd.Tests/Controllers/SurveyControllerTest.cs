using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Services;
using Xunit.Abstractions;

namespace SherpaBackEnd.Tests.Controllers;

public class SurveyControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SurveyControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ShouldInvokeGetAllSurveysFromSurveyService()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, logger);
        
        await surveyController.GetAllSurveysFromTeam(teamId);
        
        surveyService.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once());
    }

    [Fact]
    public async Task ShouldReturnOkObjectResultIfNoErrorsFoundWhenGettingAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
    }

    [Fact]
    public async Task ShouldReturnAListOfSurveysWhenCalledGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        Assert.IsAssignableFrom<IEnumerable<Survey>>(responseObject.Value);
    }

    [Fact]
    public async Task ShouldSerializeCorrectlyListOfSurveysWhenCallingGetAllSurveysFromTeam()
    {
        var surveysEmptyList = new List<Survey>();
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);

        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ReturnsAsync(surveysEmptyList);
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        
        surveyService.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once());
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        var actualSurveys = Assert.IsAssignableFrom<IEnumerable<Survey>>(responseObject.Value);
        
        Assert.Equal(JsonConvert.SerializeObject(surveysEmptyList), JsonConvert.SerializeObject(actualSurveys));
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfAllSurveysFromTeamCannotBeRetrieved()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);
        var notSuccessfulGettingAllSurveysFromTeam = new ConnectionToRepositoryUnsuccessfulException("Cannot perform get all teams function.");
        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId))
            .ThrowsAsync(notSuccessfulGettingAllSurveysFromTeam);
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, logger);

        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<ObjectResult>(allSurveysFromTeam.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }
}