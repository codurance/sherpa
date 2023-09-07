using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);
    public Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId);
    public Task<Survey> GetSurveyById(Guid expectedSurveyId);
    public Task<IEnumerable<IQuestion>> GetSurveyQuestionsBySurveyId(Guid expectedSurveyId);
}