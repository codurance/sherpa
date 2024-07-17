namespace SherpaFrontEnd.Dtos.Analysis;

public class Metrics
{
    public GeneralMetrics General { get; }

    public Metrics(GeneralMetrics general)
    {
        General = general;
    }
}