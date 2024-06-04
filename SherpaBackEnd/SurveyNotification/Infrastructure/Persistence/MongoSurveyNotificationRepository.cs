using Microsoft.Extensions.Options;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

public class MongoSurveyNotificationRepository: ISurveyNotificationsRepository
{
    public MongoSurveyNotificationRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        throw new NotImplementedException();
    }

    public Task CreateSurveyNotification(Domain.SurveyNotification surveyNotification)
    {
        throw new NotImplementedException();
    }
}