namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task<IEnumerable<SurveyTemplate>> DeprecatedGetTemplates();
    bool DeprecatedIsTemplateExist(Guid templateId);
    void CreateSurvey(Survey.Survey survey);
}