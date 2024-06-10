using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

public class MongoSurveyNotificationRepository : ISurveyNotificationsRepository
{
    private readonly IMongoCollection<MSurveyNotification> _surveyNotificationCollection;
    private readonly IMongoCollection<MSurvey> _surveyCollection;
    private readonly IMongoCollection<MTeamMember> _teamMemberCollection;
    private readonly IMongoCollection<MTeam> _teamCollection;
    private readonly IMongoCollection<BsonDocument> _templateCollection;

    public MongoSurveyNotificationRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _surveyNotificationCollection =
            mongoDatabase.GetCollection<MSurveyNotification>(databaseSettings.Value.SurveyNotificationCollectionName);
        _surveyCollection = mongoDatabase.GetCollection<MSurvey>(databaseSettings.Value.SurveyCollectionName);
        _teamMemberCollection =
            mongoDatabase.GetCollection<MTeamMember>(databaseSettings.Value.TeamMembersCollectionName);
        _teamCollection = mongoDatabase.GetCollection<MTeam>(databaseSettings.Value.TeamsCollectionName);
        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(databaseSettings.Value.TemplateCollectionName);
    }

    public async Task CreateManySurveyNotification(List<Domain.SurveyNotification> surveyNotifications)
    {
        var databaseSurveyNotifications = surveyNotifications
            .Select(notification => MSurveyNotification.FromSurveyNotification(notification)).ToList();
        await _surveyNotificationCollection.InsertManyAsync(databaseSurveyNotifications);
    }

    public async Task<Domain.SurveyNotification> GetSurveyNotificationById(Guid surveyNotificationId)
    {
        var mSurveyNotification = await _surveyNotificationCollection
            .Find((notification => notification.Id.Equals(surveyNotificationId))).FirstOrDefaultAsync();

        return await PopulateSurveyNotification(mSurveyNotification);
    }

    private async Task<Domain.SurveyNotification> PopulateSurveyNotification(MSurveyNotification mSurveyNotification)
    {
        var mTeamMember = await _teamMemberCollection
            .Find(Builders<MTeamMember>.Filter.Eq(member => member.Id, mSurveyNotification.TeamMember))
            .FirstOrDefaultAsync();
        var mSurvey = await _surveyCollection.Find(Builders<MSurvey>.Filter
            .Eq(survey1 => survey1.Id, mSurveyNotification.Survey)).FirstOrDefaultAsync();
        var mTeam = await _teamCollection.Find(Builders<MTeam>.Filter.Eq(team => team.Id, mSurvey.Team))
            .FirstOrDefaultAsync();
        var mTemplate = await _templateCollection.Find(template => template["name"] == mSurvey.Template)
            .FirstOrDefaultAsync();
        var teamMember = MTeamMember.ToTeamMember(mTeamMember);
        var mTeamMembers = await _teamMemberCollection
            .FindAsync(Builders<MTeamMember>.Filter.In("_id", mTeam.Members)).Result.ToListAsync();

        var team = MTeam.ToTeam(mTeam, mTeamMembers.Select(MTeamMember.ToTeamMember).ToList());
        var template = MTemplate.ParseTemplate(mSurvey.Template, mTemplate);
        var survey = MSurvey.ToSurvey(mSurvey, team, template);

        return MSurveyNotification.ToSurveyNotification(mSurveyNotification, survey, teamMember);
    }
}