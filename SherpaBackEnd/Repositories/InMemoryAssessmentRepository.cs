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

    public async Task<IEnumerable<Assessment>> GetAssessments(Guid teamId)
    {
        return await Task.FromResult(_assessments.FindAll(a => a.TeamId == teamId));
    }

    public async Task<Assessment?> GetAssessment(Guid teamId, Guid templateId)
    {
        return await Task.FromResult(_assessments.FirstOrDefault(a => a.TeamId == teamId && a.TemplateId == templateId));
    }

    public Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate)
    {
        var assessmentIndex = _assessments
            .FindIndex(a => a.TeamId == assessmentToUpdate.TeamId && a.TemplateId == assessmentToUpdate.TemplateId);

        _assessments[assessmentIndex] = assessmentToUpdate;

        return Task.FromResult(_assessments[assessmentIndex]);
    }
}