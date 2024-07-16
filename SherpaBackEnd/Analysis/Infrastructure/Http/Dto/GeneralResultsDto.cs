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
}