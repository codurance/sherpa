using SherpaBackEnd.SurveyNotification;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.SurveyNotification.Application;

public interface ISurveyNotificationService
{
    Task CreateNotificationsFor(CreateSurveyNotificationsDto surveyId);
}