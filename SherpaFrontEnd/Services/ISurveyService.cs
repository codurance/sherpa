using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;

namespace SherpaFrontEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);

    public Task<SurveyWithoutQuestions?> GetSurveyById(Guid id);
    
    public Task<List<Survey>?> GetAllSurveysByTeam(Guid teamId);
}