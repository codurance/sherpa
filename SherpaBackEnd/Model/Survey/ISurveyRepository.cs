namespace SherpaBackEnd.Model.Survey;

public interface ISurveyRepository
{
    public Task CreateSurvey();
    public Task<IEnumerable<Survey>> GetAllSurveys();
    public Task<Survey?> GetSurveyById();
}