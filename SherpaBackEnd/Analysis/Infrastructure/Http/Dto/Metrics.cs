namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class Metrics
{
    public double Average { get; }
    public double Aspirational { get; }

    public Metrics(double average, double aspirational)
    {
        Average = average;
        Aspirational = aspirational;
    }
}