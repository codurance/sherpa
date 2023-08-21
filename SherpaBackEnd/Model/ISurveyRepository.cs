namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task<IEnumerable<SurveyTemplate>> DeprecatedGetTemplates();
    bool DeprecatedIsTemplateExist(Guid templateId);
    Task CreateSurvey(Survey.Survey survey);
    Task<IEnumerable<Survey.Survey>> GetAllSurveysFromTeam(Guid teamId);
    Task<Survey.Survey?> GetSurveyById(Guid surveyId);
}