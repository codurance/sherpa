namespace SherpaFrontEnd.Dtos.Analysis;

public class LastSurveyCategoryMetric
{
    public string Name { get; }
    public double Average { get; }
    public bool HasImproved { get; }

    public LastSurveyCategoryMetric(string name, double average, bool hasImproved)
    {
        Name = name;
        Average = average;
        HasImproved = hasImproved;
    }
}