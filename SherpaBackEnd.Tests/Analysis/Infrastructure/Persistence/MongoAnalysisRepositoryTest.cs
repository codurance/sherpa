using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Tests.Helpers.Analysis;
using SherpaBackEnd.Tests.Helpers.Analysis.Infrastructure;

namespace SherpaBackEnd.Tests.Analysis.Infrastructure.Persistence;

public class MongoAnalysisRepositoryTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private IMongoCollection<BsonDocument> _surveyCollection;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument>? _teamCollection;
    private IMongoCollection<BsonDocument>? _teamMemberCollection;
    private IMongoCollection<BsonDocument>? _templateCollection;
    private IMongoDatabase? _mongoDatabase;

    [Fact]
    public async Task ShouldReturnAnEmptyAnalysisIfThereAreNoSurveys()
    {
        await InitialiseDatabaseClientAndCollections();
        await MongoAnalysisHelper.InsertTemplate(_templateCollection);


        var teamId = Guid.NewGuid();

        var analysisRepository = new MongoAnalysisRepository(_databaseSettings);
        var actual = await analysisRepository.GetAnalysisByTeamIdAndTemplateName(teamId, "Hackman Model");

        Assert.Empty(actual.Categories);
        Assert.Empty(actual.Surveys);
    }

    [Fact]
    public async Task ShouldReturnAnalysisFromAGivenTeamIdAndTemplateName()
    {
        var teamId = Guid.NewGuid();

        await InitialiseDatabaseClientAndCollections();
        await MongoAnalysisHelper.InsertTemplate(_templateCollection);
        await MongoAnalysisHelper.InsertSurveyWithTeamId(_surveyCollection, teamId);

        var options = new List<string>() { "1", "2", "3", "4", "5" };
        
        var expected = new HackmanAnalysis(AnalysisHelper.BuildASurveyWithMultipleParticipants());


        var analysisRepository = new MongoAnalysisRepository(_databaseSettings);
        var actual = await analysisRepository.GetAnalysisByTeamIdAndTemplateName(teamId, "Hackman Model");

        CustomAssertions.StringifyEquals(expected, actual);
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }

    private async Task InitialiseDatabaseClientAndCollections()
    {
        await _mongoDbContainer.StartAsync();

        _databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });

        var mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

        _mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);

        _templateCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);

        _surveyCollection = _mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }

}