using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services.Email;

namespace SherpaBackEnd.Services;

public class AssessmentService : IAssessmentService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IAssessmentRepository _assessmentRepository;
    private readonly IEmailService _emailService;

    public AssessmentService(ISurveyRepository surveyRepository, IAssessmentRepository assessmentRepository, IEmailService emailService)
    {
        _surveyRepository = surveyRepository;
        _assessmentRepository = assessmentRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<SurveyTemplate>> GetTemplates()
    {
        return await _surveyRepository.DeprecatedGetTemplates();
    }

    public async Task<Assessment?> AddAssessment(Guid teamId, Guid templateId, string name)
    {
        if (_surveyRepository.DeprecatedIsTemplateExist(templateId))
        {
            var newAssessment = new Assessment(teamId, templateId, name);
            _assessmentRepository.AddAssessment(newAssessment);

            return await _assessmentRepository.GetAssessment(teamId, templateId);
        }

        return null;
    }

    public async Task<IEnumerable<Assessment>> GetAssessments()
    {
        return await _assessmentRepository.GetAssessments();
    }

    public async Task<IEnumerable<Assessment>> GetAssessments(Guid teamId)
    {
        return await _assessmentRepository.GetAssessments(teamId);
    }

    public async Task<Assessment?> GetAssessment(Guid teamId, Guid templateId)
    {
        return await _assessmentRepository.GetAssessment(teamId, templateId);
    }

    public async Task<Assessment> UpdateAssessment(Assessment assessmentToUpdate)
    {
        var updatedAssessment = await _assessmentRepository.UpdateAssessment(assessmentToUpdate);
        if (updatedAssessment is not null && updatedAssessment.Surveys.Any())
        {
            await _emailService.SendEmail("Test Email", updatedAssessment.GetLastSurveyEmails());
        }
        return updatedAssessment;
    }
}