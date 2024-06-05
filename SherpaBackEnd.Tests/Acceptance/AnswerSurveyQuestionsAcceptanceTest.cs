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
using SherpaBackEnd.Shared.Infrastructure.Persistence;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Domain.Exceptions;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Survey.Infrastructure.Persistence;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Persistence;
using SherpaBackEnd.Template.Infrastructure.Persistence;
using SherpaBackEnd.Tests.Builders;
using static SherpaBackEnd.Tests.Builders.SurveyBuilder;
using static SherpaBackEnd.Tests.Builders.TeamBuilder;
using static SherpaBackEnd.Tests.Builders.TeamMemberBuilder;
using static SherpaBackEnd.Tests.Builders.TemplateBuilder;

namespace SherpaBackEnd.Tests.Acceptance;

public class AnswerSurveyQuestionsAcceptanceTest : IDisposable
{
    private readonly IContainer _mongoDbContainer = new ContainerBuilder()
        .WithImage("mongodb/mongodb-community-server:latest")
        .WithPortBinding(27017, true).Build();

    private readonly ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private IOptions<DatabaseSettings> _databaseSettings;
    private IMongoCollection<BsonDocument> _templateCollection;

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
    }

    [Fact]
    public async Task TeamMemberShouldBeAbleToRespondToSurveyAndResponsesShouldBeStoredInDatabase()
    {
        await InitializeDbClientAndCollections();
        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveyService = new SurveyService(surveyRepository, teamRepository, templateRepository);
        var surveyController = new SurveyController(surveyService, _logger);
        var surveyId = Guid.NewGuid();
        var teamMemberId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var coachId = Guid.NewGuid();
        var coach = new User.Domain.User(coachId, "Lucia");
        var teamMember = ATeamMember()
            .WithId(teamMemberId)
            .Build();
        var team = ATeam()
            .WithId(teamId)
            .WithTeamMembers(new List<TeamMember>() { teamMember })
            .Build();
        var template = ATemplate()
            .Build();
        var templateWithoutQuestions = ATemplate().BuildWithoutQuestions();
        var deadline = new DateTime(2024, 06, 24).ToUniversalTime();
        var survey = ASurvey()
            .WithId(surveyId)
            .WithTeam(team)
            .WithTemplate(template)
            .WithDeadline(deadline)
            .WithCoach(coach)
            .Build();

        await teamRepository.AddTeamAsync(team);
        await teamRepository.AddTeamMemberToTeamAsync(team.Id, teamMember);
        await surveyRepository.CreateSurvey(survey);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        // Given: A TeamMember has responded to a survey
        var response = new SurveyResponse(teamMemberId);
        var answerSurveyDto = new AnswerSurveyDto(surveyId, teamMemberId, response);
        var expectedSurvey = ASurvey()
            .WithId(surveyId)
            .WithTeam(team)
            .WithTemplateWithoutQuestions(templateWithoutQuestions)
            .WithResponses(new List<SurveyResponse>() { response })
            .WithCoach(coach)
            .WithDeadline(deadline)
            .BuildWithoutQuestions();

        // When: The responses are submitted 
        var actionResult = await surveyController.AnswerSurvey(answerSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);

        // Then: The responses should be stored in the survey
        var retrievedSurvey = await surveyController.GetSurveyWithoutQuestionsById(surveyId);
        var okObjectResult = Assert.IsType<OkObjectResult>(retrievedSurvey.Result);
        Assert.NotNull(okObjectResult.Value);
        CustomAssertions.StringifyEquals(expectedSurvey, okObjectResult.Value);
    }

    [Fact]
    public async Task TeamMemberShouldNotBeAbleToCreateTwoResponsesForASurvey()
    {
        await InitializeDbClientAndCollections();
        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveyService = new SurveyService(surveyRepository, teamRepository, templateRepository);
        var surveyController = new SurveyController(surveyService, _logger);
        var surveyId = Guid.NewGuid();
        var teamMemberId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var coachId = Guid.NewGuid();
        var coach = new User.Domain.User(coachId, "Lucia");
        var teamMember = ATeamMember()
            .WithId(teamMemberId)
            .Build();
        var team = ATeam()
            .WithId(teamId)
            .WithTeamMembers(new List<TeamMember>() { teamMember })
            .Build();
        var template = ATemplate()
            .Build();
        var templateWithoutQuestions = ATemplate().BuildWithoutQuestions();
        var deadline = new DateTime(2024, 06, 24).ToUniversalTime();
        SurveyResponse firstResponse = new SurveyResponse(teamMemberId);
        var survey = ASurvey()
            .WithId(surveyId)
            .WithTeam(team)
            .WithTemplate(template)
            .WithDeadline(deadline)
            .WithResponses(new List<SurveyResponse>() { firstResponse })
            .WithCoach(coach)
            .Build();

        await teamRepository.AddTeamAsync(team);
        await teamRepository.AddTeamMemberToTeamAsync(team.Id, teamMember);
        await surveyRepository.CreateSurvey(survey);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        // Given: A TeamMember has already saved a response to a survey
        var secondResponse = new SurveyResponse(teamMemberId);
        var answerSurveyDto = new AnswerSurveyDto(surveyId, teamMemberId, secondResponse);
        var expectedMessage = new SurveyAlreadyAnsweredException(teamMemberId).Message;

        // When: A second response is submitted 
        var actionResult = await surveyController.AnswerSurvey(answerSurveyDto);

        // Then: The controller should respond with bad request
        var badRequestResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal(expectedMessage, badRequestResult.Value);
    }

    [Fact]
    public async Task TeamMemberShouldNotBeAbleToAnswerASurveyNotAssignedToThem()
    {
        await InitializeDbClientAndCollections();
        var templateRepository = new MongoTemplateRepository(_databaseSettings);
        var teamRepository = new MongoTeamRepository(_databaseSettings);
        var surveyRepository = new MongoSurveyRepository(_databaseSettings);
        var surveyService = new SurveyService(surveyRepository, teamRepository, templateRepository);
        var surveyController = new SurveyController(surveyService, _logger);
        var surveyId = Guid.NewGuid();
        var teamMemberId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var coachId = Guid.NewGuid();
        var coach = new User.Domain.User(coachId, "Lucia");
        var teamMember = ATeamMember()
            .WithId(teamMemberId)
            .Build();
        var team = ATeam()
            .WithId(teamId)
            .Build();
        var template = ATemplate()
            .Build();
        var templateWithoutQuestions = ATemplate().BuildWithoutQuestions();
        var deadline = new DateTime(2024, 06, 24).ToUniversalTime();
        var survey = ASurvey()
            .WithId(surveyId)
            .WithTeam(team)
            .WithTemplate(template)
            .WithDeadline(deadline)
            .WithCoach(coach)
            .Build();

        await teamRepository.AddTeamAsync(team);
        await surveyRepository.CreateSurvey(survey);

        await _templateCollection.InsertOneAsync(new BsonDocument
        {
            { "name", template.Name },
            { "minutesToComplete", template.MinutesToComplete },
            { "questions", new BsonArray(template.Questions) }
        });

        // Given: A TeamMember is not assigned to a survey
        var response = new SurveyResponse(teamMemberId);
        var answerSurveyDto = new AnswerSurveyDto(surveyId, teamMemberId, response);
        var expectedMessage = new SurveyNotAssignedToTeamMemberException(teamMemberId).Message;

        // When: A response is submitted
        var actionResult = await surveyController.AnswerSurvey(answerSurveyDto);

        // Then: The controller should respond with bad request
        var badRequestResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal(expectedMessage, badRequestResult.Value);
    }

    public void Dispose()
    {
        _mongoDbContainer.StopAsync();
    }
}