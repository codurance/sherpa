using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Analysis.Infrastructure.Http;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.ConfigurationVariables.Domain;

namespace SherpaBackEnd.Tests.Acceptance;

public class AnalysisAcceptanceTest
{
    [Fact]
    public async Task ShouldBeAbleToRetrieveTheGeneralResultsFromATeamId()
    {
        var categories = new[]
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });

        var series = new List<ColumnSeries<double>>() { survey1, survey2 };
        var columnChart = new ColumnChart<double>(categories, series, 1);
        var expected = new GeneralResultsDto(columnChart);
        var analysisController = new AnalysisController();
        
        var response = await analysisController.GetGeneralResults();
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var generalResults = Assert.IsType<GeneralResultsDto>(resultObject.Value);
        
        Assert.Equal(expected, generalResults);
    }
}