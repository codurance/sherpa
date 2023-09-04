using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;

namespace SherpaBackEnd.Repositories.Mongo;

public class MongoSurveyRepository: ISurveyRepository
{
    private readonly IMongoCollection<MSurvey> _surveyCollection;

    public MongoSurveyRepository(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _surveyCollection = mongoDatabase.GetCollection<MSurvey>(
            databaseSettings.Value.SurveyCollectionName);
    }
    
    public Task<IEnumerable<SurveyTemplate>> DeprecatedGetTemplates()
    {
        throw new NotImplementedException();
    }

    public bool DeprecatedIsTemplateExist(Guid templateId)
    {
        throw new NotImplementedException();
    }

    public Task CreateSurvey(Survey survey)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Survey>> GetAllSurveysFromTeam(Guid teamId)
    {
        throw new NotImplementedException();
    }

    public Task<Survey?> GetSurveyById(Guid surveyId)
    {
        throw new NotImplementedException();
    }
}