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
            "Enabling Structure",
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
        var survey = new List<SurveyMetric>()
        {
            new SurveyMetric("Survey 1", 0.50),
            new SurveyMetric("Survey 2", 0.66),
            new SurveyMetric("Survey 3", 0.70),
            new SurveyMetric("Survey 4", 0.83)
        };
        var lastSurveyCategoryMetrics = new List<LastSurveyCategoryMetric>()
        {
            new LastSurveyCategoryMetric("Real team", 0.74, false),
            new LastSurveyCategoryMetric("Compelling direction", 0.64, false),
            new LastSurveyCategoryMetric("Expert coaching", 0.81, true),
            new LastSurveyCategoryMetric("Supportive org coaching", 0.88, true)
        };

        var metrics = new Metrics(generalMetrics, survey, lastSurveyCategoryMetrics);

        var generalResults = new GeneralResultsDto(columnChart, metrics);
        return generalResults;
    }
}