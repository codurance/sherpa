using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Analysis.Application;

public class AnalysisService : IAnalysisService
{
    public async Task<GeneralResultsDto> GetGeneralResults()
    {
        var categories = new[]
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.6, 0.2, 0.4, 0.7 });
        var survey3 = new ColumnSeries<double>("Survey 3", new List<double>() { 0.6, 0.6, 0.3, 0.5, 0.6 });
        var survey4 = new ColumnSeries<double>("Survey 4", new List<double>() { 0.6, 0.6, 0.5, 0.5, 0.8 });
        var survey5 = new ColumnSeries<double>("Survey 5", new List<double>() { 0.8, 0.7, 0.5, 0.5, 0.9 });

        var series = new List<ColumnSeries<double>>() { survey1, survey2, survey3, survey4, survey5 };
        var columnChart = new ColumnChart<double>(categories, series, 1);
        var generalResults = new GeneralResultsDto(columnChart);

        return generalResults;
    }
}