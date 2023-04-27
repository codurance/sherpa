using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public interface IAssessmentService
{
    Task<IEnumerable<SurveyTemplate>> GetTemplates();
    Assessment? AddAssessment(Guid groupId, Guid templateId, string name);

    Task<IEnumerable<Assessment>> GetAssessments();
    Task<Assessment?> GetAssessment(Guid groupId, Guid templateId);
}