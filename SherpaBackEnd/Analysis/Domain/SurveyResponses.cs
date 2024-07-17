using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResponses<T>
{
    public string Title { get; }
    
    public List<Participant<T>> Participants { get; }
    
    public TemplateAnalysis Template { get; }
    
    public SurveyResponses(string title, List<Participant<T>> participants, TemplateAnalysis template)
    {
        Title = title;
        Participants = participants;
        Template = template;
    }
    
}