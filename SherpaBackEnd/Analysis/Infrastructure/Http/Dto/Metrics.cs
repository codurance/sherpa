namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class Metrics
{
    public GeneralMetrics General { get; }

    public Metrics(GeneralMetrics general)
    {
        General = general;
    }
}