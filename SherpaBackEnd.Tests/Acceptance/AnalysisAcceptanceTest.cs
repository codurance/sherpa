using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Infrastructure.Http;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Analysis.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Tests.Helpers.Analysis;
using SherpaBackEnd.Tests.Helpers.Analysis.Infrastructure;

namespace SherpaBackEnd.Tests.Acceptance;

public class AnalysisAcceptanceTest
{
    
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();
 
    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _templateCollection;
    private IMongoCollection<BsonDocument> _surveyCollection;
    private IMongoDatabase? _mongoDatabase;

    [Fact]
    public async Task ShouldBeAbleToRetrieveTheGeneralResultsFromATeamId()
    {
        await InitialiseDatabaseClientAndCollections();
        
        var teamId = Guid.NewGuid();

        await MongoAnalysisHelper.InsertTemplate(_templateCollection);
        await MongoAnalysisHelper.InsertSurveyWithTeamId(_surveyCollection, teamId);
        
        var expected = AnalysisHelper.BuildASingleSurveyGeneralResultsDto();
        
        var analysisRepository = new MongoAnalysisRepository(_databaseSettings);
        var analysisService = new AnalysisService(analysisRepository);
        var analysisController = new AnalysisController(analysisService);
        
        var response = await analysisController.GetGeneralResults(teamId);
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var generalResults = Assert.IsType<GeneralResultsDto>(resultObject.Value);
        
        CustomAssertions.StringifyEquals(expected.Metrics, generalResults.Metrics);
        Assert.Equal(expected.ColumnChart.Categories.Count, generalResults.ColumnChart.Categories.Count);
        CustomAssertions.StringifyEquals(expected.ColumnChart.Config, generalResults.ColumnChart.Config);
        CustomAssertions.StringifyEquals(expected.ColumnChart.Series, generalResults.ColumnChart.Series);

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