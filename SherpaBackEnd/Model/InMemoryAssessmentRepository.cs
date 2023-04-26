using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public class InMemoryAssessmentRepository : IAssessmentRepository
{
    private List<Assessment> _assessments;

    public InMemoryAssessmentRepository()
    {
        _assessments = new List<Assessment>();
    }

    public void AddAssessment(Assessment assessment)
    {
        _assessments.Add(assessment);
    }

    public IEnumerable<Assessment> GetAssessments()
    {
        throw new NotImplementedException();
    }

    public Assessment? GetAssessment(Guid groupId, Guid templateId)
    {
        return _assessments.FirstOrDefault(a => a.GroupId == groupId && a.TemplateId == templateId);
    }
}