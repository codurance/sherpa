using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories;

namespace SherpaBackEnd.Services;

public class SurveysService : ISurveysService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly InMemoryTeamRepository _inMemoryTeamRepository;
    private readonly InMemoryFilesTemplateRepository _inMemoryFilesTemplateRepository;

    public SurveysService(InMemorySurveyRepository surveyRepository, InMemoryTeamRepository inMemoryTeamRepository,
        InMemoryFilesTemplateRepository inMemoryFilesTemplateRepository)
    {
        _surveyRepository = surveyRepository;
        _inMemoryTeamRepository = inMemoryTeamRepository;
        _inMemoryFilesTemplateRepository = inMemoryFilesTemplateRepository;
    }
}