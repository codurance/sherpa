namespace SherpaBackEnd.Survey.Domain.Persistence;

public interface ISurveyRepository
{
    Task CreateSurvey(Survey survey);
    Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId);
    Task<Survey?> GetSurveyById(Guid surveyId);
}