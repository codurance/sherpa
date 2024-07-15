using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    public async Task<GeneralResultsDto> GetGeneralResults()
    {
        var categories = new[]
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });

        var series = new List<ColumnSeries<double>>() { survey1, survey2 };
        var columnChart = new ColumnChart<double>(categories, series, 1);
        var generalResults = new GeneralResultsDto(columnChart);

        return generalResults;
    }
}