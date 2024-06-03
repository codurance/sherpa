using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http;

[ApiController]
[Route("survey-notifications")]
public class SurveyNotificationController
{
    private readonly ISurveyNotificationService _surveyNotificationService;

    public SurveyNotificationController(ISurveyNotificationService surveyNotificationService)
    {
        _surveyNotificationService = surveyNotificationService;
    }
    
    [HttpPost]
    public async Task<IActionResult> LaunchSurvey(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        try
        {
            await _surveyNotificationService.CreateNotificationsFor(createSurveyNotificationsDto);
            return new CreatedResult("", null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}