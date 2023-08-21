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

    public async Task<IEnumerable<SurveyTemplate>> DeprecatedGetTemplates()
    {
        return await Task.FromResult<IEnumerable<SurveyTemplate>>(_templates.Values);
    }

    public bool DeprecatedIsTemplateExist(Guid templateId)
    {
        return _templates.ContainsKey(templateId);
    }

    public async Task CreateSurvey(Survey survey)
    {
        _surveys.Add(survey);
    }

    public async Task<Survey?> GetSurveyById(Guid surveyId)
    {
        return _surveys.Find(survey => survey.Id == surveyId);
    }
    
    public async Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        var surveysFromTeamId = _surveys.FindAll(survey => survey.Team.Id == teamId);
        return await Task.FromResult<IEnumerable<Survey>>(surveysFromTeamId);
    }
}