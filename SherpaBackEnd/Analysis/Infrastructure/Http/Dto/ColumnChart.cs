namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class ColumnChart<T>
{
    public List<string> Categories { get; }
    public List<ColumnSeries<T>> Series { get; }
    public ColumnChartConfig<T> Config { get; }

    public ColumnChart(List<string> categories, List<ColumnSeries<T>> series, ColumnChartConfig<T> config)
    {
        Categories = categories;
        Series = series;
        Config = config;
    }
}