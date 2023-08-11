using SherpaFrontEnd.Dtos.Survey;

namespace SherpaFrontEnd.Services;

public interface ISurveyService
{
    public Task<List<Survey>?> GetAllSurveysByTeam(Guid teamId);
}