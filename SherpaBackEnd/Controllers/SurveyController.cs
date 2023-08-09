using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class SurveyController
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger _logger;

    public SurveyController(ISurveyService surveyService, ILogger<SurveyController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        try
        {
            await _surveyService.CreateSurvey(createSurveyDto);

            return new CreatedResult("", null);
        }
        catch (Exception e)
        {
            _logger.LogError(default, e, e.Message);

            return e switch
            {
                NotFoundException => new ObjectResult(e)
                    { StatusCode = StatusCodes.Status400BadRequest, Value = e.Message },
                _ => new ObjectResult(e) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
    
[HttpGet("{guid:guid}")]
public async Task<ActionResult<Survey>> GetSurveyById(Guid guid)
    {
        try
        {
            var surveyById = await _surveyService.GetSurveyById(guid);
            return new OkObjectResult(surveyById);
        }
        catch (Exception e)
        {
            _logger.LogError(default, e, e.Message);

            return e switch
            {
                NotFoundException => new ObjectResult(e)
                    { StatusCode = StatusCodes.Status404NotFound, Value = e.Message },
                _ => new ObjectResult(e) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}