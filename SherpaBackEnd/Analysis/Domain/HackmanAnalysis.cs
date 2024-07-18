namespace SherpaBackEnd.Analysis.Domain;

public class HackmanAnalysis
{
    public HashSet<string> Categories { get; }
    
    public List<SurveyResult<string>> Surveys = new();

    public double Average => GetAverage();

    public readonly double Aspirational = 0.75;

    public HackmanAnalysis(List<SurveyAnalysisData<string>> surveyAnalysisDatas)
    {
        Categories = new HashSet<string>();
        foreach (var survey in surveyAnalysisDatas)
        {
            var surveyResult = new SurveyResult<string>(survey.Title);
            foreach (var participant in survey.Participants)
            {
                foreach (var response in participant.Responses)
                {
                    Categories.Add(response.Category);
                    surveyResult.AddResponse(response);
                }
            }
            surveyResult.Categories = Categories.ToList();
            surveyResult.NumberOfParticipants = survey.Participants.Count;
            Surveys.Add(surveyResult);
        }
    }

    private double GetAverage()
    {
        if (Surveys.Count == 0) return 0;
        return Math.Round(Surveys.Aggregate(0.0, (sum, survey) => sum + survey.Average) / Surveys.Count, 2);
    }
}