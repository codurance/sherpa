using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Repositories;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    
    public SurveyService(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public Task CreateSurvey()
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

    public async Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        try
        {
            return await _surveyRepository.GetAllSurveysFromTeam(teamId);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }
}