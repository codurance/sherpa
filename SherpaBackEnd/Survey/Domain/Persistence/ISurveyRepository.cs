namespace SherpaBackEnd.Survey.Domain.Persistence;

public interface ISurveyRepository
{
    Task CreateSurvey(Survey survey);
    Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId);
    Task<IEnumerable<Survey>> GetAllSurveysWithResponsesFromTeam(Guid teamId);
    Task<Survey?> GetSurveyById(Guid surveyId);
    Task Update(Survey survey);
}