using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class AssessmentController
{
    private readonly ISurveyService _surveyService;

    public AssessmentController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    public async Task<ActionResult<IEnumerable<SurveyTemplate>>> GetTemplates()
    {
        var templates = await _surveyService.GetTemplates();
        if (templates.Any())
        {
            return new OkObjectResult(templates);
        }

        return new NotFoundResult();
    }

    public async Task<ActionResult<Assessment>> AddAssessment(Guid groupId, Guid templateId)
    {
        if (await _surveyService.IsTemplateExist(templateId))
        {
            return new OkObjectResult(new Assessment(groupId, templateId));
        }

        return new BadRequestResult();
    }
}