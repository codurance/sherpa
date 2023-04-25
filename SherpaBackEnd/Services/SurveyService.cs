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
        return await _surveyRepository.GetTemplates();
    }

    public async Task<bool> IsTemplateExist(Guid templateId)
    {
        return await _surveyRepository.IsTemplateExist(templateId);
    }
}