using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Survey.Application;

public interface ISurveyService
{
    public Task CreateSurvey(CreateSurveyDto createSurveyDto);
    public Task<IEnumerable<Domain.Survey>> GetAllSurveysFromTeam(Guid teamId);
    public Task<SurveyWithoutQuestions> GetSurveyWithoutQuestionsById(Guid expectedSurveyId);
    public Task<IEnumerable<IQuestion>> GetSurveyQuestionsBySurveyId(Guid expectedSurveyId);
}