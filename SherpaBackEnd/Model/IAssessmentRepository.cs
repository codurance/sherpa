using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IAssessmentRepository
{
    void AddAssessment(Assessment assessment);
    Task<IEnumerable<Assessment>> GetAssessments();
    Task<Assessment?> GetAssessment(Guid groupId, Guid templateId);

    Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate);
}