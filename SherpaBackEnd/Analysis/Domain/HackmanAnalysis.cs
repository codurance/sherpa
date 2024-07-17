namespace SherpaBackEnd.Analysis.Domain;

public class HackmanAnalysis
{
    public HashSet<string> Categories { get; }
    
    public List<SurveyResult> surveys;
    public double Average { get; }
    public readonly double Aspirational = 0.75;

    public HackmanAnalysis(List<SurveyResponses> surveyResponses)
    {
        Categories = new HashSet<string>();
        foreach (var survey in surveyResponses)
        {
            foreach (var response in survey.Responses)
            {
                Categories.Add(survey.Template.Questions[response.Number].Category);
            }
        }
    }
}