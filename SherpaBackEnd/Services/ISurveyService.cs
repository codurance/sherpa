using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model.Survey;

namespace SherpaBackEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);
    public Task<IEnumerable<Survey>> GetAllSurveys();
    public Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId);
    public Task<Survey> GetSurveyById(Guid expectedSurveyId);
}