using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class SurveyController
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger _logger;

    public SurveyController(ISurveyService surveyService, ILogger<SurveyController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

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
                NotFoundException => new ObjectResult(e) { StatusCode = StatusCodes.Status400BadRequest, Value = e.Message},
                _ => new ObjectResult(e) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }

    public async Task<ActionResult<Survey>> GetSurveyById(Guid surveyId)
    {
        try
        {
            var surveyById = await _surveyService.GetSurveyById(surveyId);
            return new OkObjectResult(surveyById);
        }
        catch (Exception e)
        {
            _logger.LogError(default, e, e.Message);

            return e switch
            {
                NotFoundException => new ObjectResult(e) { StatusCode = StatusCodes.Status404NotFound, Value = e.Message },
                _ => new ObjectResult(e) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}