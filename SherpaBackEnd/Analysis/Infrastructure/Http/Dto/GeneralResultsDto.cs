namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class GeneralResultsDto
{
    public ColumnChart<double> ColumnChart { get; init; }

    public GeneralResultsDto(ColumnChart<double> columnChart)
    {
        ColumnChart = columnChart;
    }
}