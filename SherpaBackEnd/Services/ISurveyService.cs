using SherpaBackEnd.Model.Survey;

namespace SherpaBackEnd.Services;

public interface ISurveyService
{
    public Task CreateSurvey();
    public Task<IEnumerable<Survey>> GetAllSurveys();
    public Task<Survey?> GetSurveyById();
}