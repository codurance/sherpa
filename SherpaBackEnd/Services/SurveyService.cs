using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Repositories;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly ITemplateRepository _templateRepository;

    public SurveyService(ISurveyRepository surveyRepository, ITeamRepository teamRepository,
        ITemplateRepository templateRepository)
    {
        _surveyRepository = surveyRepository;
        _teamRepository = teamRepository;
        _templateRepository = templateRepository;
    }

    public Task CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Survey>> GetAllSurveys()
    {
        throw new NotImplementedException();
    }

    public Task<Survey?> GetSurveyById()
    {
        throw new NotImplementedException();
    }

    Task ISurveyService.CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        throw new NotImplementedException();
    }
}