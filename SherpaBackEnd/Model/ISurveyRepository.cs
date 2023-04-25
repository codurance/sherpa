namespace SherpaBackEnd.Model;

public interface ISurveyRepository
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
}