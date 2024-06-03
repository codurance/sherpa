using SherpaBackEnd.SurveyNotification;

namespace SherpaBackEnd.SurveyNotification.Application;

public interface ISurveyNotificationService
{
    Task CreateNotificationsFor(Guid surveyId);
}