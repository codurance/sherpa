using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Template;

namespace SherpaBackEnd.Repositories.Mongo;

public class MongoTemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<BsonDocument> _abstractTemplatesCollection;

    public MongoTemplateRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _abstractTemplatesCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.Value.TemplateCollectionName);
    }

    public async Task<Template[]> GetAllTemplatesAsync()
    {
        var response = new List<Template>();

        var rawTemplates = await _abstractTemplatesCollection.Find(template => true).ToListAsync();

        foreach (var rawTemplate in rawTemplates)
        {
            var templateName = rawTemplate.GetElement("name").Value.AsString;
            switch (templateName)
            {
                case "Hackman Model":
                    response.Add(MTemplate.ParseTemplate(templateName, rawTemplate));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return response.ToArray();
    }

    public async Task<Template> GetTemplateByName(string templateName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("name", templateName);
        var template = await _abstractTemplatesCollection.Find(filter).FirstOrDefaultAsync();

        return MTemplate.ParseTemplate(templateName, template);
    }
}