namespace SherpaFrontEnd.Dtos.Analysis;

public class ColumnSeries<T>
{
    public string Name { get; }
    public List<T> Data { get; }

    public ColumnSeries(string name, List<T> data)
    {
        Name = name;
        Data = data;
    }
}