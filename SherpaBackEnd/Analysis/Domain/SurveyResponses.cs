using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResponses<T>
{
    public string Title { get; }
    public List<Response<T>> Responses { get; }
    
    public TemplateAnalysis Template { get; }
    
    public SurveyResponses(string title, List<Response<T>> responses, TemplateAnalysis template)
    {
        Title = title;
        Responses = responses;
        Template = template;
    }
    
}