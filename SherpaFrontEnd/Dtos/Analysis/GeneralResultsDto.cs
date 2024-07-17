namespace SherpaFrontEnd.Dtos.Analysis;

public class GeneralResultsDto
{
    public ColumnChart<double> ColumnChart { get; }
    public Metrics Metrics { get; }

    public GeneralResultsDto(ColumnChart<double> columnChart, Metrics metrics)
    {
        ColumnChart = columnChart;
        Metrics = metrics;
    }
}