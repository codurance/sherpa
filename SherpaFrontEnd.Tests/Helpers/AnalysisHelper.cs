using SherpaFrontEnd.Dtos.Analysis;

namespace BlazorApp.Tests.Helpers;

public static class AnalysisHelper
{
    public static GeneralResultsDto BuildGeneralResultsDto()
    {
        var categories = new string[]
        {
            "Real team",
            "Compelling direction",
            "Expert coaching",
            "Enable structure",
            "Supportive org coaching"
        };
        var columnChartConfig = new ColumnChartConfig<double>(1.0, 0.25, 2);
        var firstSurvey = new ColumnSeries<double>("Survey 1", new List<double>()
        {
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        });
        var secondSurvey = new ColumnSeries<double>("Survey 2", new List<double>()
        {
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        });
        var series = new List<ColumnSeries<double>>() { firstSurvey, secondSurvey };
        var columnChart = new ColumnChart<double>(categories, series, columnChartConfig);

        var generalMetrics = new GeneralMetrics(0.50, 0.75);
        var metrics = new Metrics(generalMetrics);
        
        var generalResults = new GeneralResultsDto(columnChart, metrics);
        return generalResults;
    }
}