namespace SherpaBackEnd.Model;

public class InMemorySurveyRepository : ISurveyRepository
{
    private Dictionary<Guid, SurveyTemplate> _templates;
    private List<Survey.Survey> _surveys;

    public InMemorySurveyRepository()
    {
        _surveys = new List<Survey.Survey>();
        _templates = new Dictionary<Guid, SurveyTemplate>();

        var template = new SurveyTemplate("Hackman");
        
        _templates.Add(template.Id, template);
    }

    public async Task<IEnumerable<SurveyTemplate>> DeprecatedGetTemplates()
    {
        return await Task.FromResult<IEnumerable<SurveyTemplate>>(_templates.Values);
    }

    public bool DeprecatedIsTemplateExist(Guid templateId)
    {
        return _templates.ContainsKey(templateId);
    }

    public async Task CreateSurvey(Survey.Survey survey)
    {
        _surveys.Add(survey);
    }

    public async Task<Survey.Survey?> GetSurveyById(Guid surveyId)
    {
        return _surveys.Find(survey => survey.Id == surveyId);
    }
}