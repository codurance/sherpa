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
        catch (NotFoundException e)
        {
            _logger.LogError(default, e, e.Message);
            return new ObjectResult(e)
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Value = e.Message
            };
        }
        catch (Exception e)
        {
            _logger.LogError(default, e, e.Message);
            return new ObjectResult(e)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    public Task<ActionResult<Survey>> GetSurveyById(Guid surveyId)
    {
        throw new NotImplementedException();
    }
}