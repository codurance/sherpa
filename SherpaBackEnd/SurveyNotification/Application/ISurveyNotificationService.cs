using SherpaBackEnd.Sur;

namespace SherpaBackEnd.SurveyNotification.Application;

public interface ISurveyNotificationService
{
    Task CreateNotificationsFor(Guid surveyId);
}