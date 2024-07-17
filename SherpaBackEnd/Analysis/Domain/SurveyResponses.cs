using SherpaBackEnd.Survey.Domain;

namespace SherpaBackEnd.Analysis.Domain;

public class SurveyResponses
{
    public List<QuestionResponse> Responses { get; }
    
    public TemplateAnalysis Template { get; }
    
    public SurveyResponses(List<QuestionResponse> responses, TemplateAnalysis template)
    {
        Responses = responses;
        Template = template;
    }
    
}