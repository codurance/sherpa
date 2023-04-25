using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public interface ISurveyService
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
    Task<bool> IsTemplateExist(Guid templateId);
}