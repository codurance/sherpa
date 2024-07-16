namespace SherpaFrontEnd.Dtos.Analysis;

public class GeneralMetrics
{
    public double Average { get; }
    public double Aspirational { get; }

    public GeneralMetrics(double average, double aspirational)
    {
        Average = average;
        Aspirational = aspirational;
    }
}