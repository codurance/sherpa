using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public interface IAssessmentService
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
    Task<Assessment?> AddAssessment(Guid teamId, Guid templateId, string name);

    Task<IEnumerable<Assessment>> GetAssessments();
    Task<IEnumerable<Assessment>> GetAssessments(Guid teamId);
    Task<Assessment?> GetAssessment(Guid teamId, Guid templateId);
    Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate);
}