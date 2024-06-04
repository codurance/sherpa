namespace SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

public interface ISurveyNotificationsRepository
{
    Task CreateSurveyNotification(Domain.SurveyNotification surveyNotification);
}