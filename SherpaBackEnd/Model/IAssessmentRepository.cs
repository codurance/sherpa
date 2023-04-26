using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IAssessmentRepository
{
    void AddAssessment(Assessment assessment);
    Task<IEnumerable<Assessment>> GetAssessments();
    Assessment? GetAssessment(Guid groupId, Guid templateId);
}