using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Application;
using SherpaBackEnd.Analysis.Infrastructure.Http;
using SherpaBackEnd.Analysis.Infrastructure.Http.Dto;
using SherpaBackEnd.Analysis.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Persistence;

namespace SherpaBackEnd.Tests.Acceptance;

public class AnalysisAcceptanceTest
{
    
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();
 
    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _teamCollection;
    private IMongoCollection<BsonDocument> _teamMemberCollection;
    private IMongoCollection<BsonDocument> _templateCollection;
    private IMongoCollection<BsonDocument> _surveyCollection;
    
    private async Task InitializeDbClientAndCollections()
    {
        await _mongoDbContainer.StartAsync();
        _databaseSettings = Options.Create(new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TeamsCollectionName = "Teams",
            TeamMembersCollectionName = "TeamMembers",
            SurveyCollectionName = "Surveys",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{_mongoDbContainer.GetMappedPublicPort(27017)}"
        });

        var mongoClient = new MongoClient(_databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _databaseSettings.Value.DatabaseName);
        
        _templateCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TemplateCollectionName);
        
        _surveyCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.SurveyCollectionName);
    }
    
    
    [Fact]
    public async Task ShouldBeAbleToRetrieveTheGeneralResultsFromATeamId()
    {
        await InitializeDbClientAndCollections();
        
        var categories = new[]
            { "Real Team", "Compelling Direction", "Expert Coaching", "Enable Structure", "Supportive Coaching" };
        var survey1 = new ColumnSeries<double>("Survey 1", new List<double>() { 0.5, 0.5, 0.2, 0.1, 0.8 });
        var survey2 = new ColumnSeries<double>("Survey 2", new List<double>() { 0.5, 0.6, 0.2, 0.4, 0.7 });
        var survey3 = new ColumnSeries<double>("Survey 3", new List<double>() { 0.6, 0.6, 0.3, 0.5, 0.6 });
        var survey4 = new ColumnSeries<double>("Survey 4", new List<double>() { 0.6, 0.6, 0.5, 0.5, 0.8 });
        var survey5 = new ColumnSeries<double>("Survey 5", new List<double>() { 0.8, 0.7, 0.5, 0.5, 0.9 });

        var series = new List<ColumnSeries<double>>() { survey1, survey2, survey3, survey4, survey5 };
        var columnChart = new ColumnChart<double>(categories, series, new ColumnChartConfig<double>(1,0.25,2));
        var metrics = new Metrics(0.9,0.75);
        var expected = new GeneralResultsDto(columnChart, metrics);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var templateAnalysisRepository = new MongoTemplateAnalysisRepository(_databaseSettings);
        var analysisService = new AnalysisService(surveyRepository, templateAnalysisRepository);
        var analysisController = new AnalysisController(analysisService);
        var teamId = Guid.NewGuid();
        
        var response = await analysisController.GetGeneralResults(teamId);
        
        var resultObject = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        var generalResults = Assert.IsType<GeneralResultsDto>(resultObject.Value);
        
        CustomAssertions.StringifyEquals(expected, generalResults);
    }
}