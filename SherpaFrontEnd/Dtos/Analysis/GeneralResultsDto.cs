namespace SherpaFrontEnd.Dtos.Analysis;

public class GeneralResultsDto
{
    public ColumnChart<double> ColumnChart { get; }

    public GeneralResultsDto(ColumnChart<double> columnChart)
    {
        ColumnChart = columnChart;
    }
}