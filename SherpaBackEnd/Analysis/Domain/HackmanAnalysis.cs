namespace SherpaBackEnd.Analysis.Domain;

public class HackmanAnalysis
{
    public HashSet<string> Categories { get; }
    
    public List<SurveyResult<string>> Surveys = new();
    public double Average { get; } = 0.75;
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
}