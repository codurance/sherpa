namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
    bool IsTemplateExist(Guid templateId);
    Task<IEnumerable<Survey.Survey>> GetAllSurveysFromTeam(Guid teamId);
}