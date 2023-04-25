using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;

    public SurveyService(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<IEnumerable<SurveyTemplate>> GetTemplates()
    {
        // var templates = _surveyRepository.GetTemplates(TODO);
        return new List<SurveyTemplate>{new("hackman")};
    }
}