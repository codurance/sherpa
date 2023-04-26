using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IAssessmentRepository
{
    void AddAssessment(Assessment assessment);
    IEnumerable<Assessment> GetAssessments();
    Assessment? GetAssessment(Guid groupId, Guid templateId);
}