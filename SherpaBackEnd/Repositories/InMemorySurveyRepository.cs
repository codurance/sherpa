using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Repositories;

public class InMemorySurveyRepository : ISurveyRepository
{
    private readonly List<Survey> _surveys;
    private Dictionary<Guid, SurveyTemplate> _templates;

    public InMemorySurveyRepository()
    {
        _surveys = new List<Survey>();
        _templates = new Dictionary<Guid, SurveyTemplate>();

        var template = new SurveyTemplate("Hackman");
        
        _templates.Add(template.Id, template);
    }

    public InMemorySurveyRepository(List<Survey> surveys)
    {
        _surveys = surveys;
    }

    public async Task<IEnumerable<SurveyTemplate>> GetTemplates()
    {
        return await Task.FromResult<IEnumerable<SurveyTemplate>>(_templates.Values);
    }

    public bool IsTemplateExist(Guid templateId)
    {
        return _templates.ContainsKey(templateId);
    }

    public async Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        var surveysFromTeamId = _surveys.FindAll(survey => survey.Team.Id == teamId);
        return await Task.FromResult<IEnumerable<Survey>>(surveysFromTeamId);
    }
}