using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Template.Domain;

namespace SherpaBackEnd.Survey.Infrastructure.Http;

[ApiController]
[Route("")]
public class SurveyController
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger _logger;

    public SurveyController(ISurveyService surveyService, ILogger<SurveyController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

    [HttpPost("survey")]
    public async Task<ActionResult> CreateSurvey(CreateSurveyDto createSurveyDto)
    {
        try
        {
            await _surveyService.CreateSurvey(createSurveyDto);
            return new CreatedResult("", null);
        }
        catch (Exception error)
        {
            _logger.LogError(default, error, error.Message);

            return error switch
            {
                NotFoundException => new ObjectResult(error)
                    { StatusCode = StatusCodes.Status400BadRequest, Value = error.Message },
                _ => new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }

    [HttpGet("team/{teamId:guid}/surveys")]
    public async Task<ActionResult<IEnumerable<Domain.Survey>>> GetAllSurveysFromTeam(Guid teamId)
    {
        try
        {
            var allSurveysFromTeam = await _surveyService.GetAllSurveysFromTeam(teamId);
            return new OkObjectResult(allSurveysFromTeam);
        }
        catch (Exception error)
        {
            _logger.LogError(default, error, error.Message);

            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [HttpGet("survey/{guid:guid}")]
    public async Task<ActionResult<Domain.Survey>> GetSurveyWithoutQuestionsById(Guid guid)
    {
        try
        {
            var surveyById = await _surveyService.GetSurveyWithoutQuestionsById(guid);
            return new OkObjectResult(surveyById);
        }
        catch (Exception error)
        {
            _logger.LogError(default, error, error.Message);

            return error switch
            {
                NotFoundException => new ObjectResult(error)
                    { StatusCode = StatusCodes.Status404NotFound, Value = error.Message },
                _ => new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }

    [HttpGet("survey/{guid:guid}/questions")]
    public async Task<ActionResult<IEnumerable<IQuestion>>> GetSurveyQuestionsBySurveyId(Guid guid)
    {
        var surveysQuestions = await _surveyService.GetSurveyQuestionsBySurveyId(guid);
        return new OkObjectResult(surveysQuestions);
    }
}