namespace SherpaBackEnd.Model;

public class InMemorySurveyRepository : ISurveyRepository
{
    private Dictionary<Guid, SurveyTemplate> _templates;

    public InMemorySurveyRepository()
    {
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

    public void CreateSurvey(Survey.Survey survey)
    {
        throw new NotImplementedException();
    }
}