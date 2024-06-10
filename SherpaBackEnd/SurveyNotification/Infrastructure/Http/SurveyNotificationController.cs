using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Responses;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http;

[ApiController]
[Route("survey-notifications")]
public class SurveyNotificationController
{
    private readonly ISurveyNotificationService _surveyNotificationService;
    private readonly ILogger<SurveyNotificationController> _logger;

    public SurveyNotificationController(ISurveyNotificationService surveyNotificationService,
        ILogger<SurveyNotificationController> logger)
    {
        _surveyNotificationService = surveyNotificationService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> LaunchSurvey(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        try
        {
            await _surveyNotificationService.LaunchSurveyNotificationsFor(createSurveyNotificationsDto);
            return new CreatedResult("", null);
        }
        catch (Exception error)
        {
            return error switch
            {
                NotFoundException => new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status400BadRequest, Value = error.Message
                },
                _ => new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status500InternalServerError, Value = error.Message
                }
            };
        }
    }

    [HttpGet("/{surveyNotificationId:guid}")]
    public async Task<IActionResult> GetSurveyNotification(Guid surveyNotificationId)
    {
        try
        {
            var surveyNotification = await _surveyNotificationService.GetSurveyNotification(surveyNotificationId);
            var responseBody = SurveyNotificationResponse.FromSurveyNotification(surveyNotification);
            return new OkObjectResult(responseBody);
        }
        catch (Exception error)
        {
            Console.WriteLine(error);
            return new BadRequestResult();
        }
    }
}