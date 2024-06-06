using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

public class MongoSurveyNotificationRepository : ISurveyNotificationsRepository
{
    private readonly IMongoCollection<MSurveyNotification> _surveyNotificationCollection;

    public MongoSurveyNotificationRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _surveyNotificationCollection =
            mongoDatabase.GetCollection<MSurveyNotification>(databaseSettings.Value.SurveyNotificationCollectionName);
    }

    public async Task CreateManySurveyNotification(List<Domain.SurveyNotification> surveyNotifications)
    {
        var databaseSurveyNotifications = surveyNotifications
            .Select(notification => MSurveyNotification.FromSurvey(notification)).ToList();
        await _surveyNotificationCollection.InsertManyAsync(databaseSurveyNotifications);
    }
}