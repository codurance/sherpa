using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IAssessmentsDataService
{
    public Task<List<Assessment>?> GetAssessments();
}