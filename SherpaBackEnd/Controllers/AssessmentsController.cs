using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;


[ApiController]
[Route("[controller]")]
public class AssessmentsController
{
    private readonly ISurveyService _surveyService;

    public AssessmentsController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SurveyTemplate>>> GetTemplates()
    {
        var templates = await _surveyService.GetTemplates();
        if (templates.Any())
        {
            return new OkObjectResult(templates);
        }

        return new NotFoundResult();
    }

    
    [HttpPost]
    public async Task<ActionResult<Assessment>> AddAssessment(Guid groupId, Guid templateId)
    {
        if (await _surveyService.IsTemplateExist(templateId))
        {
            return new OkObjectResult(new Assessment(groupId, templateId));
        }

        return new BadRequestResult();
    }
}