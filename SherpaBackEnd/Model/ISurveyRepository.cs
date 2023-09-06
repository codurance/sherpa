namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task CreateSurvey(Survey.Survey survey);
    Task<IEnumerable<Survey.Survey>> GetAllSurveysFromTeam(Guid teamId);
    Task<Survey.Survey?> GetSurveyById(Guid surveyId);
}