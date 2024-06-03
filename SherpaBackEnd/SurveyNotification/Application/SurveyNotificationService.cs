using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.SurveyNotification.Application;

public class SurveyNotificationService : ISurveyNotificationService
{
    private readonly ISurveyRepository _surveyRepositoryObject;

    public SurveyNotificationService(ISurveyRepository surveyRepositoryObject)
    {
        _surveyRepositoryObject = surveyRepositoryObject;
    }

    public async Task LaunchSurveyNotificationsFor(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        await _surveyRepositoryObject.GetSurveyById(createSurveyNotificationsDto.SurveyId);
    }
}