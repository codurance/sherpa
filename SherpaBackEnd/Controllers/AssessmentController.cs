using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class AssessmentController
{
    public AssessmentController(ISurveyService surveyService)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<IEnumerable<SurveyTemplate>>> GetTemplates()
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<Assessment>> AddAssessment(Guid groupId, Guid templateId)
    {
        throw new NotImplementedException();
    }
}