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

    public async Task<IEnumerable<Assessment>> GetAssessments()
    {
        return await Task.FromResult(_assessments);
    }

    public async Task<Assessment?> GetAssessment(Guid groupId, Guid templateId)
    {
        return await Task.FromResult(_assessments.FirstOrDefault(a => a.GroupId == groupId && a.TemplateId == templateId));
    }

    public async Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate)
    {
        var assessmentIndex = _assessments
            .FindIndex(a => a.GroupId == assessmentToUpdate.GroupId && a.TemplateId == assessmentToUpdate.TemplateId);

        _assessments[assessmentIndex] = assessmentToUpdate;

        return _assessments[assessmentIndex];
    }
}