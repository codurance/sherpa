using SherpaFrontEnd.Dtos;
using SherpaFrontEnd.Dtos.Survey;

namespace SherpaFrontEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);

    public Task<SurveyWithoutQuestions?> GetSurveyWithoutQuestionsById(Guid id);
    
    public Task<List<Survey>?> GetAllSurveysByTeam(Guid teamId);

    public Task<List<Question>?> GetSurveyQuestionsBySurveyId(Guid surveyId);
}