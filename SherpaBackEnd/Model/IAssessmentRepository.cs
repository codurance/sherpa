using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IAssessmentRepository
{
    void AddAssessment(Assessment assessment);
    Task<IEnumerable<Assessment>> GetAssessments();
    Task<IEnumerable<Assessment>> GetAssessments(Guid teamId);
    Task<Assessment?> GetAssessment(Guid teamId, Guid templateId);

    Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate);
}