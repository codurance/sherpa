namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
    Task<bool> IsTemplateExist(Guid templateId);
}