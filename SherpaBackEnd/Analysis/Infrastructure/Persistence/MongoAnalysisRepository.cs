using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Domain.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

namespace SherpaBackEnd.Analysis.Infrastructure.Persistence;

public class MongoAnalysisRepository : IAnalysisRepository
{
    
    private readonly IMongoCollection<BsonDocument> _abstractTemplatesCollection;

    public MongoAnalysisRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _abstractTemplatesCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TemplateCollectionName);
    }

    public Task<TemplateAnalysis> GetTemplateAnalysisByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<HackmanAnalysis> GetAnalysisByTeamIdAndTemplateName(Guid teamId, string name)
    {
        throw new NotImplementedException();
    }
}