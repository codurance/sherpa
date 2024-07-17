using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Domain.Exceptions;
using SherpaBackEnd.Analysis.Infrastructure.Http;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Tests.Helpers.Analysis;

namespace SherpaBackEnd.Tests.Analysis.Infrastructure.Http;

public class AnalysisControllerTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveTheGeneralResultsFromATeamId()
    {
        var expected = AnalysisHelper.BuildGeneralResultsDto();
        
        var analysisServiceMock = new Mock<IAnalysisService>();
        var teamId = Guid.NewGuid();
        analysisServiceMock.Setup(analysisService => analysisService.GetGeneralResults(teamId)).ReturnsAsync(expected);
        var analysisController = new AnalysisController(analysisServiceMock.Object);
        
        var response = await analysisController.GetGeneralResults(teamId);
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var generalResults = Assert.IsType<GeneralResultsDto>(resultObject.Value);
        
        CustomAssertions.StringifyEquals(expected, generalResults);
    }

    [Fact]
    public async Task ShouldReturnAnErrorIfTheTeamIdDoesNotExist()
    {
        var analysisServiceMock = new Mock<IAnalysisService>();
        var teamId = Guid.NewGuid();
        var teamNotFoundException = new TeamNotFoundException("Team ID is not found");
        analysisServiceMock.Setup(analysisService => analysisService.GetGeneralResults(teamId)).ThrowsAsync(teamNotFoundException);
        var analysisController = new AnalysisController(analysisServiceMock.Object);
        
        var response = await analysisController.GetGeneralResults(teamId);
        
        var resultObject = Assert.IsType<ObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status404NotFound, resultObject.StatusCode);
    }

}