using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Repositories;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Acceptance;

public class SurveysAcceptanceTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveATeamSurveys()
    {
        var teamId = Guid.NewGuid();
        var emptySurveyList = new List<Survey>() { };
        var inMemorySurveyRepository = new InMemorySurveyRepository(emptySurveyList);
        var surveyService = new SurveyService(inMemorySurveyRepository);
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService, logger);

        var teamSurveys = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<OkObjectResult>(teamSurveys.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        Assert.Equal(emptySurveyList, resultObject.Value);
    }
}