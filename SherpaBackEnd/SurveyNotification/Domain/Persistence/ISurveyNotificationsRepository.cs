namespace SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

public interface ISurveyNotificationsRepository
{
    Task CreateManySurveyNotification(List<Domain.SurveyNotification> surveyNotifications);
}