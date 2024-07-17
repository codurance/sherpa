using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Test.Helpers;
using SherpaBackEnd.Analysis.Domain;
using SherpaBackEnd.Analysis.Infrastructure.Persistence;
using SherpaBackEnd.Shared.Infrastructure.Persistence;

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
        await InsertTemplate();

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
        await InsertTemplate();
        await InsertSurveyWithTeamId(teamId);

        var options = new List<string>() { "1", "2", "3", "4", "5" };
        
        var expected = new HackmanAnalysis(new List<SurveyAnalysisData<string>>()
        {
            new("Super Survey", new List<Participant<string>>()
            {
                new(new List<Response<string>>()
                {
                    new("Real Team", "1", false, options),
                    new("Enabling Structure", "5", false, options),
                    new("Enabling Structure", "5", false, options)
                }),
                new(new List<Response<string>>()
                {
                    new("Real Team", "5", false, options),
                    new("Enabling Structure", "1", false, options),
                    new("Enabling Structure", "5", false, options)
                })
            })
        });


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

    private async Task InsertTemplate()
    {
        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", "Hackman Model" },
            {
                "questions", new BsonArray()
                {
                    new BsonDocument()
                    {
                        { "component", "Real Team" },
                        { "position", "1" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        },
                    },
                    new BsonDocument()
                    {
                        { "component", "Enabling Structure" },
                        { "position", "2" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        },
                    },
                    new BsonDocument()
                    {
                        { "component", "Enabling Structure" },
                        { "position", "3" },
                        { "reverse", BsonBoolean.False },
                        {
                            "responses", new BsonDocument()
                            {
                                { "SPANISH", new BsonArray() },
                                {
                                    "ENGLISH", new BsonArray()
                                        { @"1", "2", "3", $"4", "5" }
                                },
                            }
                        }
                    }
                }
            },
            { "minutesToComplete", 30 }
        });
    }

    private async Task InsertSurveyWithTeamId(Guid teamId)
    {
        await _surveyCollection.InsertOneAsync(new BsonDocument
        {
            { "_id", "8caba1b3-c931-4b98-95c9-58ebac0045db" },
            { "Title", "Super Survey" },
            { "Status", 0 },
            { "Deadline", new DateTime() },
            { "Description", "Sample description" },
            { "Team", teamId.ToString() },
            { "Template", "Hackman Model" },
            {
                "Responses", new BsonArray()
                {
                    new BsonDocument()
                    {
                        { "TeamMemberId", "8a5f4cce-018a-4a6c-8901-44b729973c1d" },
                        {
                            "Answers", new BsonArray()
                            {
                                new BsonDocument
                                {
                                    { "Number", 1 },
                                    { "Answer", "1" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 2 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 3 },
                                    { "Answer", "5" }
                                },
                            }
                        }
                    },
                    new BsonDocument()
                    {
                        { "TeamMemberId", "8a5f4cce-018a-4a6c-8901-432121212bb1" },
                        {
                            "Answers", new BsonArray()
                            {
                                new BsonDocument
                                {
                                    { "Number", 1 },
                                    { "Answer", "5" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 2 },
                                    { "Answer", "1" }
                                },
                                new BsonDocument
                                {
                                    { "Number", 3 },
                                    { "Answer", "5" }
                                },
                            }
                        }
                    }
                }
            },
            {
                "Coach", new BsonDocument
                {
                    { "_id", "92fb4bb7-ef6a-44b4-b48c-d5c751d6d22d" },
                    { "Name", "Lucia" }
                }
            }
        });
    }
}