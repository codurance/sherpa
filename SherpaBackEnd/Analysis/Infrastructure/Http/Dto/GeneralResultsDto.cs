using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class GeneralResultsDto
{
    public ColumnChart<double> ColumnChart { get; init; }
    public Metrics Metrics { get; }

    public GeneralResultsDto(ColumnChart<double> columnChart, Metrics metrics)
    {
        ColumnChart = columnChart;
        Metrics = metrics;
    }

    public static GeneralResultsDto FromAnalysis(HackmanAnalysis hackmanAnalysis)
    {
        List<ColumnSeries<double>> series = hackmanAnalysis.Surveys
            .Select(survey =>
                new ColumnSeries<double>(survey.Title, MapSurveyResultToColumnSeries(survey)))
            .ToList();

        return new GeneralResultsDto(
            new ColumnChart<double>(
                hackmanAnalysis.Categories,
                series,
                new ColumnChartConfig<double>(1, 0.1, 1)),
            new Metrics(new GeneralMetrics(hackmanAnalysis.Average, hackmanAnalysis.Aspirational)));
    }

    private static List<double> MapSurveyResultToColumnSeries(SurveyResult<string> survey)
    {
        return survey.Categories.Where(category => survey.Categories.Contains(category))
            .Select(category => survey.CategoryResults[category].PercentageOfPositives).ToList();
    }
}