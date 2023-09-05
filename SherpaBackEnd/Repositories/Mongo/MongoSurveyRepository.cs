using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;

namespace SherpaBackEnd.Repositories.Mongo;

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
            databaseSettings.Value.TemplateCollectionName);

        _teamMemberCollection = mongoDatabase.GetCollection<MTeamMember>(
            databaseSettings.Value.TemplateCollectionName);
    }

    public async Task CreateSurvey(Survey survey)
    {
        await _surveyCollection.InsertOneAsync(MSurvey.FromSurvey(survey));
    }

    public async Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        var mSurveys = await _surveyCollection.Find(survey => survey.Team == teamId).ToListAsync();

        return mSurveys == null
            ? new List<Survey>()
            : mSurveys.Select(PopulateMSurvey).Select(task => task.Result).Where(survey => survey != null)
                .Select(survey => survey!);
    }

    public async Task<Survey?> GetSurveyById(Guid surveyId)
    {
        var mSurvey = await _surveyCollection.Find(survey => survey.Id == surveyId).FirstOrDefaultAsync();

        if (mSurvey == null)
        {
            return null;
        }

        return await PopulateMSurvey(mSurvey);
    }

    private async Task<Survey?> PopulateMSurvey(MSurvey mSurvey)
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