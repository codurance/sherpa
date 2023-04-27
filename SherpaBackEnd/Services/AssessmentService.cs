using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class AssessmentService : IAssessmentService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IAssessmentRepository _assessmentRepository;

    public AssessmentService(ISurveyRepository surveyRepository, IAssessmentRepository assessmentRepository)
    {
        _surveyRepository = surveyRepository;
        _assessmentRepository = assessmentRepository;
    }

    public async Task<IEnumerable<SurveyTemplate>> GetTemplates()
    {
        return await _surveyRepository.GetTemplates();
    }

    public async Task<Assessment?> AddAssessment(Guid groupId, Guid templateId, string name)
    {
        if (_surveyRepository.IsTemplateExist(templateId))
        {
            var newAssessment = new Assessment(groupId, templateId, name);
            _assessmentRepository.AddAssessment(newAssessment);

            return await _assessmentRepository.GetAssessment(groupId, templateId);
        }

        return null;
    }

    public async Task<IEnumerable<Assessment>> GetAssessments()
    {
        return await _assessmentRepository.GetAssessments();
    }

    public async Task<Assessment?> GetAssessment(Guid groupId, Guid templateId)
    {
        return await _assessmentRepository.GetAssessment(groupId, templateId);
    }

    public async Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate)
    {
        return await _assessmentRepository.UpdateAssessment(assessmentToUpdate);
    }
}