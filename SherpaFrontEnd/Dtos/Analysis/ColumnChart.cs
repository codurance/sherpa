namespace SherpaFrontEnd.Dtos.Analysis;

public class ColumnChart<T>
{
    public string[] Categories { get; }
    public List<ColumnSeries<T>> Series { get; }
    public ColumnChartConfig<T> Config { get; }

    public ColumnChart(string[] categories, List<ColumnSeries<T>> series, ColumnChartConfig<T> config)
    {
        Categories = categories;
        Series = series;
        Config = config;
    }
}