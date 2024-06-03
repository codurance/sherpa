using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.SurveyNotification.Application;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Http;

[ApiController]
[Route("survey-notifications")]
public class SurveyNotificationController
{
    public SurveyNotificationController(ISurveyNotificationService surveyNotificationService)
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<object> LaunchSurvey(object launchSurveyDto)
    {
        throw new NotImplementedException();
    }
}