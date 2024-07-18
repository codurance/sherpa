using MongoDB.Driver.Linq;

namespace SherpaBackEnd.Analysis.Domain;

public class HackmanAnalysis
{
    public List<string> Categories => GetCategories();

    private readonly HashSet<string> _categories = new();

    public readonly List<SurveyResult<string>> Surveys = new();

    public double Average => GetAverage();

    public readonly double Aspirational = 0.75;

    public HackmanAnalysis(List<SurveyAnalysisData<string>> surveyAnalysisDatas)
    {
        foreach (var survey in surveyAnalysisDatas)
        {
            var surveyResult = new SurveyResult<string>(survey.Title);
            foreach (var participant in survey.Participants)
            {
                foreach (var response in participant.Responses)
                {
                    _categories.Add(response.Category);
                    surveyResult.AddResponse(response);
                }
            }

            surveyResult.Categories = Categories;
            surveyResult.NumberOfParticipants = survey.Participants.Count;
            Surveys.Add(surveyResult);
        }
    }

    private List<string> GetCategories()
    {
        var sortedCategories = new List<string>
        {
            "Real Team",
            "Compelling Direction",
            "Enabling Structure",
            "Supportive Organizational Context",
            "Expert Coaching"
        };
        return sortedCategories.Where(category => _categories.Contains(category)).ToList();
    }

    private double GetAverage()
    {
        if (Surveys.Count == 0) return 0;
        return Math.Round(Surveys.Aggregate(0.0, (sum, survey) => sum + survey.Average) / Surveys.Count, 2);
    }
}