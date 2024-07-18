namespace SherpaBackEnd.Analysis.Infrastructure.Http.Dto;

public class Metrics
{
    public GeneralMetrics General { get; }
    public List<SurveyMetric> Surveys { get; }
    public List<LastSurveyCategoryMetric> LastSurveyCategories { get; }

    public Metrics(GeneralMetrics general)
    {
        General = general;
        // TODO: remove hardcoded values
        Surveys = new List<SurveyMetric>()
        {
            new SurveyMetric("Survey 1", 0.78),
            new SurveyMetric("Survey 2", 0.75),
            new SurveyMetric("Survey 3", 0.81),
            new SurveyMetric("Survey 4", 0.72)
        };
        LastSurveyCategories = new List<LastSurveyCategoryMetric>()
        {
            new LastSurveyCategoryMetric("Real team", 0.72, false),
            new LastSurveyCategoryMetric("Enable Structure", 0.68, false),
            new LastSurveyCategoryMetric("Expert coaching", 0.78, true),
            new LastSurveyCategoryMetric("Supportive Org. Context", 0.75, true),
            new LastSurveyCategoryMetric("Compelling Direction", 0.81, true)
        };
    }
}