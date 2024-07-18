using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

namespace SherpaBackEnd.Analysis.Infrastructure.Persistence;

public class MongoAnalysisRepository : IAnalysisRepository
{
    private readonly IMongoCollection<MSurvey> _surveyCollection;
    private readonly IMongoCollection<BsonDocument> _templateCollection;

    public MongoAnalysisRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _surveyCollection = mongoDatabase.GetCollection<MSurvey>(
            databaseSettings.Value.SurveyCollectionName);

        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TemplateCollectionName);
    }

    public async Task<HackmanAnalysis> GetAnalysisByTeamIdAndTemplateName(Guid teamId, string templateName)
    {
        var mSurveys = await _surveyCollection.Find(survey => survey.Team == teamId && survey.Responses.Count > 0).ToListAsync();
        var surveyRawTemplate = await _templateCollection.Find(template => template["name"] == templateName)
            .FirstOrDefaultAsync();
        
        var templateAnalysis = MTemplateAnalysis.ParseTemplate(templateName, surveyRawTemplate);
        var surveyAnalysisDatas =
            mSurveys.Select(s => MSurveyAnalysisData.ToSurveyAnalysisData(s, templateAnalysis)).ToList();
        
        return new HackmanAnalysis(surveyAnalysisDatas);
    }
}