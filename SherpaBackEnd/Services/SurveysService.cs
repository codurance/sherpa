using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class SurveysService : ISurveysService
{
    private readonly ISurveyRepository _surveyRepository;
    public SurveysService(InMemorySurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }
}