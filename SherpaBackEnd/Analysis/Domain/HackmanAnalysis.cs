namespace SherpaBackEnd.Analysis.Domain;

public class HackmanAnalysis
{
    public HashSet<string> Categories { get; }
    
    public List<SurveyResult<string>> Surveys = new();
    public double Average { get; }
    public readonly double Aspirational = 0.75;

    public HackmanAnalysis(List<SurveyResponses<string>> surveyResponses)
    {
        Categories = new HashSet<string>();
        foreach (var survey in surveyResponses)
        {
            var surveyResult = new SurveyResult<string>(survey.Title);
            foreach (var response in survey.Responses)
            {
                Categories.Add(response.Category);
                surveyResult.AddNewResponseForCategory(response);
            }
            surveyResult.Categories = Categories.ToList();
            surveyResult.NumberOfParticipants = survey.Responses.Count;
            Surveys.Add(surveyResult);
        }
        
        
    }
}