namespace SherpaFrontEnd.Dtos.Analysis;

public class ColumnChart<T>
{
    public string[] Categories { get; }
    public List<ColumnSeries<T>> Series { get; }
    public T MaxValue { get; }

    public ColumnChart(string[] categories, List<ColumnSeries<T>> series, T maxValue)
    {
        Categories = categories;
        Series = series;
        MaxValue = maxValue;
    }
}