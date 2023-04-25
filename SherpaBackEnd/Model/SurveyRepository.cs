namespace SherpaBackEnd.Model;

public class SurveyRepository : ISurveyRepository
{
    private Dictionary<Guid, SurveyTemplate> _templates;

    public SurveyRepository()
    {
        _templates = new Dictionary<Guid, SurveyTemplate>();

        var template = new SurveyTemplate("Hackman");
        
        _templates.Add(template.Id, template);
    }

    public async Task<IEnumerable<SurveyTemplate>> GetTemplates()
    {
        return await Task.FromResult<IEnumerable<SurveyTemplate>>(_templates.Values);
    }

    public async Task<bool> IsTemplateExist(Guid templateId)
    {
        return await Task.FromResult(_templates.ContainsKey(templateId));
    }
}