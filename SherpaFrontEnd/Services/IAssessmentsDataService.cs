using SherpaFrontEnd.Model;
using SherpaFrontEnd.Shared.Modals;

namespace SherpaFrontEnd.Services;

public interface IAssessmentsDataService
{
    public Task<List<Assessment>?> GetAssessments();
    public Task<List<SurveyTemplate>?> GetTemplates();
    public Task<Assessment?> AddAssessment(Guid groupId, Guid templateId, string name);
    public Task PutAssessment(Assessment assessment);
}