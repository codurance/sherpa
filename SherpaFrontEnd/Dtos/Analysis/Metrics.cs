namespace SherpaFrontEnd.Dtos.Analysis;

public class Metrics
{
    public GeneralMetrics General { get; }
    public List<SurveyMetric> Surveys { get; }
    public List<LastSurveyCategoryMetric> LastSurveyCategoryMetrics { get; }

    public Metrics(GeneralMetrics general, List<SurveyMetric> surveys, List<LastSurveyCategoryMetric> lastSurveyCategoryMetrics)
    {
        General = general;
        Surveys = surveys;
        LastSurveyCategoryMetrics = lastSurveyCategoryMetrics;
    }
}