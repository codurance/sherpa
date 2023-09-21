using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Http;
using SherpaBackEnd.Team.Infrastructure.Http.Dto;
using SherpaBackEnd.Team.Infrastructure.Persistence;

namespace SherpaBackEnd.Tests.Acceptance;

public class TeamMemberAcceptanceTest
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _teamCollection;

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

        _teamCollection = mongoDatabase.GetCollection<BsonDocument>(
            _databaseSettings.Value.TeamsCollectionName);
    }

    [Fact]
    public async Task ShouldBeAbleToAddATeamMemberToAnExistingTeamAndGetUpdatedListOfTeamMembers()
    {
        await InitializeDbClientAndCollections();

        const string teamName = "New team";
        var teamId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var newTeamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var addTeamMemberDto = new AddTeamMemberDto(teamId, newTeamMember);

        await _teamCollection.InsertOneAsync(
            new BsonDocument
            {
                { "_id", teamId.ToString() },
                { "Name", teamName },
                { "Members", new BsonArray() },
                { "IsDeleted", false }
            });

        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var teamMemberService = new TeamMemberService(teamRepository);
        var logger = Mock.Of<ILogger<TeamMemberController>>();
        var teamMemberController = new TeamMemberController(teamMemberService, logger);

        await teamMemberController.AddTeamMemberToTeamAsync(addTeamMemberDto);

        var currentTeamMembers = await teamMemberController.GetAllTeamMembersAsync(teamId);

        var okObjectResult = Assert.IsType<OkObjectResult>(currentTeamMembers.Result);
        Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        var teamMembersList = Assert.IsType<List<TeamMember>>(okObjectResult.Value);
        Assert.Contains(teamMembersList, member => member.Id == addTeamMemberDto.TeamMember.Id);
    }
}