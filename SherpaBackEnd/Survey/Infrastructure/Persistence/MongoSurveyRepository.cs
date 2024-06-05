using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Domain.Persistence;

namespace SherpaBackEnd.Survey.Infrastructure.Persistence;

public class MongoSurveyRepository : ISurveyRepository
{
    private readonly IMongoCollection<MSurvey> _surveyCollection;
    private readonly IMongoCollection<BsonDocument> _templateCollection;
    private readonly IMongoCollection<MTeam> _teamCollection;
    private readonly IMongoCollection<MTeamMember> _teamMemberCollection;

    public MongoSurveyRepository(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _surveyCollection = mongoDatabase.GetCollection<MSurvey>(
            databaseSettings.Value.SurveyCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TemplateCollectionName);

        _teamCollection = mongoDatabase.GetCollection<MTeam>(
            databaseSettings.Value.TeamsCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<MTeamMember>(
            databaseSettings.Value.TeamMembersCollectionName);
    }

    public async Task CreateSurvey(Domain.Survey survey)
    {
        await _surveyCollection.InsertOneAsync(MSurvey.FromSurvey(survey));
    }

    public async Task<IEnumerable<Domain.Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        var mSurveys = await _surveyCollection.Find(survey => survey.Team == teamId).ToListAsync();

        return mSurveys == null
            ? new List<Domain.Survey>()
            : mSurveys.Select(PopulateMSurvey).Select(task => task.Result).Where(survey => survey != null)
                .Select(survey => survey!).ToList();
    }

    public async Task<Domain.Survey?> GetSurveyById(Guid surveyId)
    {
        var mSurvey = await _surveyCollection.Find(survey => survey.Id == surveyId).FirstOrDefaultAsync();

        if (mSurvey == null)
        {
            return null;
        }

        return await PopulateMSurvey(mSurvey);
    }

    public async Task Update(Domain.Survey survey)
    {
        var mSurvey = MSurvey.FromSurvey(survey);
        var filterDefinition = Builders<MSurvey>.Filter.Eq(s => s.Id, survey.Id);
        await _surveyCollection.ReplaceOneAsync(filterDefinition, mSurvey);
    }

    private async Task<Domain.Survey?> PopulateMSurvey(MSurvey mSurvey)
    {
        var surveyMTeam = await _teamCollection.Find(Builders<MTeam>.Filter.Eq("_id", mSurvey.Team.ToString()))
            .FirstOrDefaultAsync();

        var surveyRawTemplate = await _templateCollection.Find(template => template["name"] == mSurvey.Template)
            .FirstOrDefaultAsync();

        var mTeamMembers = await _teamMemberCollection
            .FindAsync(Builders<MTeamMember>.Filter.In("_id", surveyMTeam.Members)).Result.ToListAsync();

        var surveyTemplate = MTemplate.ParseTemplate(mSurvey.Template, surveyRawTemplate);
        var surveyTeam = MTeam.ToTeam(surveyMTeam, mTeamMembers.Select(MTeamMember.ToTeamMember).ToList());

        return MSurvey.ToSurvey(mSurvey, surveyTeam, surveyTemplate);
    }
}