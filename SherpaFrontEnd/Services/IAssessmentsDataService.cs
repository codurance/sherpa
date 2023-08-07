using SherpaFrontEnd.Model;
using SherpaFrontEnd.Shared.Modals;

namespace SherpaFrontEnd.Services;

public interface IAssessmentsDataService
{
    public Task<List<Assessment>?> GetAssessments();
    public Task<List<Assessment>?> GetAssessments(Guid teamId);
    public Task<List<SurveyTemplate>?> GetTemplates();
    public Task<Assessment?> AddAssessment(Guid teamId, Guid templateId, string name);
    public Task<Assessment?> PutAssessment(Assessment assessment);
    
}