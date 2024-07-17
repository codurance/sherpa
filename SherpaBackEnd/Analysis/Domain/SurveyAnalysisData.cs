using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyAnalysisData<T>
{
    public string Title { get; }
    
    public List<Participant<T>> Participants { get; }
    
    public SurveyAnalysisData(string title, List<Participant<T>> participants)
    {
        Title = title;
        Participants = participants;
    }
    
}