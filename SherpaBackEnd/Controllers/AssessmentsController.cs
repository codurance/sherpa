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
    public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessmentsAsync()
    {
        var assessments = await _surveyService.GetAssessments();
        return new OkObjectResult(assessments);
    }

    [HttpGet("templates")]
    public async Task<ActionResult<IEnumerable<SurveyTemplate>>> GetTemplatesAsync()
    {
        var templates = await _surveyService.GetTemplates();
        if (templates.Any())
        {
            return new OkObjectResult(templates);
        }

        return new NotFoundResult();
    }

    
    [HttpPost]
    public async Task<ActionResult<Assessment>> AddAssessmentAsync(Assessment assessmentDto)
    {
        var assessment = _surveyService
            .AddAssessment(assessmentDto.GroupId, assessmentDto.TemplateId, assessmentDto.Name);
        
        if (assessment is not null)
        {
            return new OkObjectResult(assessment);
        }

        return new BadRequestResult();
    }
}