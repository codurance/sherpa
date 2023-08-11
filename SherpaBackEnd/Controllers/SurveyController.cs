using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("")]
public class SurveyController
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger<TeamController> _logger;

    public SurveyController(ISurveyService surveyService, ILogger<TeamController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

    [HttpGet("team/{teamId:guid}/surveys")]
    public async Task<ActionResult<IEnumerable<Survey>>> GetAllSurveysFromTeam(Guid teamId)
    {
        try
        {
            var allSurveysFromTeam = await _surveyService.GetAllSurveysFromTeam(teamId);
            return new OkObjectResult(allSurveysFromTeam);
        }
        catch (Exception error)
        {
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}