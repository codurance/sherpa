using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;


[ApiController]
[Route("[controller]")]
public class AssessmentsController
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentsController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessmentsAsync()
    {
        var assessments = await _assessmentService.GetAssessments();
        return new OkObjectResult(assessments);
    }

    [HttpGet("templates")]
    public async Task<ActionResult<IEnumerable<SurveyTemplate>>> GetTemplatesAsync()
    {
        var templates = await _assessmentService.GetTemplates();
        if (templates.Any())
        {
            return new OkObjectResult(templates);
        }

        return new NotFoundResult();
    }

    
    [HttpPost]
    public async Task<ActionResult<Assessment>> AddAssessmentAsync(Assessment assessmentDto)
    {
        var assessment = await _assessmentService
            .AddAssessment(assessmentDto.GroupId, assessmentDto.TemplateId, assessmentDto.Name);
        
        if (assessment is not null)
        {
            return new OkObjectResult(assessment);
        }

        return new BadRequestResult();
    }

    [HttpPut]
    public async Task<ActionResult<Assessment>> UpdateAssessmentAsync(Assessment assessmentToUpdate)
    {
        var assessment = await _assessmentService.GetAssessment(assessmentToUpdate.GroupId, assessmentToUpdate.TemplateId);
        if (assessment is null)
        {
            return new NotFoundResult();
        }

        var updateAssessment = await _assessmentService.UpdateAssessment(assessmentToUpdate);
        return new OkObjectResult(assessment);
    }
}