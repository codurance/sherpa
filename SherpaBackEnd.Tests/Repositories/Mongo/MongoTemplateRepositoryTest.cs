using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SherpaBackEnd.Model;
using SherpaBackEnd.Repositories.Mongo;

namespace SherpaBackEnd.Tests.Repositories.Mongo;

public class MongoTemplateRepositoryTest: IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    [Fact]
    public async Task ShouldReturnAllTPreloadedTemplatesThatExistInTheRepo()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };
        
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TemplateCollectionName);

        await collection.InsertOneAsync(
            new BsonDocument
            {
                { "name", "Hackman Model" },
                { "questions", new BsonArray() },
                { "minutesToComplete", 10 }
            }
        );

        var mongoTemplateRepository = new MongoTemplateRepository(Options.Create(databaseSettings));

        var templates = await mongoTemplateRepository.GetAllTemplatesAsync();
        
        Assert.Single(templates);
        Assert.Equal("Hackman Model", templates[0].Name);
        Assert.Equal(10, templates[0].MinutesToComplete);
        Assert.Empty(templates[0].Questions);
    }
    
    [Fact]
    public async Task ShouldReturnSingleTemplateWhenRequestedByName()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };
        
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        var collection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSettings.TemplateCollectionName);

        const string templateName = "Hackman Model";
        
        await collection.InsertOneAsync(
            new BsonDocument
            {
                { "name", templateName },
                { "questions", new BsonArray() },
                { "minutesToComplete", 10 }
            }
        );

        var mongoTemplateRepository = new MongoTemplateRepository(Options.Create(databaseSettings));

        var template = await mongoTemplateRepository.GetTemplateByName(templateName);
        
        Assert.Equal(templateName, template.Name);
        Assert.Equal(10, template.MinutesToComplete);
        Assert.Empty(template.Questions);
    }
    
    [Fact]
    public async Task ShouldThrowNotImplementedIfAskedForNonExistingTemplate()
    {
        await _mongoDbContainer.StartAsync();

        var mappedPublicPort = _mongoDbContainer.GetMappedPublicPort(27017);
        
        var databaseSettings = new DatabaseSettings
        {
            DatabaseName = "Sherpa",
            TemplateCollectionName = "Templates",
            ConnectionString = $"mongodb://localhost:{mappedPublicPort}"
        };
        
        const string templateName = "NonExistingTemplate";

        var mongoTemplateRepository = new MongoTemplateRepository(Options.Create(databaseSettings));

        await Assert.ThrowsAsync<NotImplementedException>(async() => await mongoTemplateRepository.GetTemplateByName(templateName));
    }

    public async void Dispose()
    {
        await _mongoDbContainer.StopAsync();
    }
}