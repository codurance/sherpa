namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResult
{
    public string Title { get; }
    public List<string> Categories { get; }
    public int NumberOfParticipants { get; }
    public Dictionary<string, CategoryResult> CategoryResults;
    
}