using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Domain.Exceptions;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Tests.Builders;
using Xunit.Abstractions;
using ISurveyRepository = SherpaBackEnd.Survey.Domain.Persistence.ISurveyRepository;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private Mock<ISurveyRepository> _surveyRepository;
    private Mock<ITeamRepository> _teamRepo;
    private Mock<ITemplateRepository> _templateRepo;
    private readonly ITestOutputHelper output;
    private SurveyService _service;

    public SurveyServiceTest(ITestOutputHelper output)
    {
        this.output = output;
        _surveyRepository = new Mock<ISurveyRepository>();
        _teamRepo = new Mock<ITeamRepository>();
        _templateRepo = new Mock<ITemplateRepository>();
        _service = new SurveyService(_surveyRepository.Object, _teamRepo.Object, _templateRepo.Object);
    }

    [Fact]
    public async Task ShouldRetrieveTemplateAndTeamFromTheirReposAndPersistSurveyOnSurveyRepoWhenCallingCreateSurvey()
    {
        var team = new Team.Domain.Team(Guid.NewGuid(), "Demo team");
        var template = new Template.Domain.Template("demo", Array.Empty<IQuestion>(), 30);

        _teamRepo.Setup(repository => repository.GetTeamByIdAsync(team.Id)).ReturnsAsync(team);
        _templateRepo.Setup(repository => repository.GetTemplateByName(template.Name)).ReturnsAsync(template);

        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), team.Id, template.Name, "Title", "Description",
            DateTime.Parse("2023-08-09T07:38:04+0000"));
        await _service.CreateSurvey(createSurveyDto);

        var expectedSurvey = new Survey.Domain.Survey(createSurveyDto.SurveyId,
            new User.Domain.User(_service.DefaultUserId, "Lucia"),
            SurveyStatus.Draft, createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description,
            new List<SurveyResponse>(), team, template);
        var surveyRepoInvocation = _surveyRepository.Invocations[0];
        var actualSurvey = surveyRepoInvocation.Arguments[0];
        CustomAssertions.StringifyEquals(expectedSurvey, actualSurvey);
    }

    [Fact]
    public async Task ShouldReturnSurveyGivenByTheRepository()
    {
        var surveyId = Guid.NewGuid();
        var expectedSurvey = new Survey.Domain.Survey(Guid.NewGuid(),
            new User.Domain.User(_service.DefaultUserId, "Lucia"), SurveyStatus.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "Description", new List<SurveyResponse>(),
            new Team.Domain.Team(Guid.NewGuid(), "Demo team"),
            new Template.Domain.Template("demo", Array.Empty<IQuestion>(), 30));
        _surveyRepository.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(expectedSurvey);

        var receivedSurveyWithoutQuestions = await _service.GetSurveyWithoutQuestionsById(surveyId);

        _surveyRepository.Verify(repository => repository.GetSurveyById(surveyId));
        Assert.Equal(expectedSurvey.Id, receivedSurveyWithoutQuestions.Id);
    }

    [Fact]
    public async Task ShouldThrowNotFoundExceptionIfRepoReturnsNull()
    {
        var surveyId = Guid.NewGuid();
        _surveyRepository.Setup(repository => repository.GetSurveyById(surveyId))
            .ReturnsAsync((Survey.Domain.Survey?)null);


        await Assert.ThrowsAsync<NotFoundException>(async () => await _service.GetSurveyWithoutQuestionsById(surveyId));
    }

    [Fact]
    public async Task ShouldCallSurveyRepositoryToGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();

        await _service.GetAllSurveysFromTeam(teamId);

        _surveyRepository.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowErrorIfConnectionToRepositoryNotSuccessful()
    {
        var teamId = Guid.NewGuid();

        _surveyRepository.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ThrowsAsync(new Exception());

        var exceptionThrown =
            await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () =>
                await _service.GetAllSurveysFromTeam(teamId));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }

    [Fact]
    public async Task ShouldCallSurveyRepositoryToGetSurveyQuestionsBySurveyId()
    {
        var surveyId = Guid.NewGuid();
        var QuestionInSpanish = "Question in spanish";
        var QuestionInEnglish = "Question in english";
        var ResponseSpanish1 = "SPA_1";
        var ResponseSpanish2 = "SPA_2";
        var ResponseSpanish3 = "SPA_3";
        var ResponseEnglish1 = "ENG_1";
        var ResponseEnglish2 = "ENG_2";
        var ResponseEnglish3 = "ENG_3";
        var Position = 1;
        var Reverse = false;

        var hackmanQuestion = new HackmanQuestion(new Dictionary<string, string>()
            {
                { Languages.SPANISH, QuestionInSpanish },
                { Languages.ENGLISH, QuestionInEnglish },
            }, new Dictionary<string, string[]>()
            {
                {
                    Languages.SPANISH, new[] { ResponseSpanish1, ResponseSpanish2, ResponseSpanish3 }
                },
                {
                    Languages.ENGLISH, new[] { ResponseEnglish1, ResponseEnglish2, ResponseEnglish3 }
                }
            }, Reverse,
            HackmanComponent.INTERPERSONAL_PEER_COACHING,
            HackmanSubcategory.DELIMITED, HackmanSubcomponent.SENSE_OF_URGENCY, Position);
        var expectedSurvey = new Survey.Domain.Survey(Guid.NewGuid(),
            new User.Domain.User(_service.DefaultUserId, "Lucia"), SurveyStatus.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "Description", new List<SurveyResponse>(),
            new Team.Domain.Team(Guid.NewGuid(), "Demo team"),
            new Template.Domain.Template("demo", new List<IQuestion>() { hackmanQuestion }, 30));
        _surveyRepository.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(expectedSurvey);

        var questionsReceived = await _service.GetSurveyQuestionsBySurveyId(surveyId);

        _surveyRepository.Verify(repository => repository.GetSurveyById(surveyId));
        Assert.Contains(hackmanQuestion, questionsReceived);
    }


    [Fact]
    public async Task ShouldRetrieveSurveyWhenAnswerSurveyIsCalled()
    {
        var template = TemplateBuilder.ATemplate().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);

        await _service.AnswerSurvey(answerSurveyDto);

        _surveyRepository.Verify(repository => repository.GetSurveyById(survey.Id));
    }

    [Fact]
    public async Task ShouldAnswerSurveyWithResponseInDto()
    {
        var template = TemplateBuilder.ATemplate().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);

        await _service.AnswerSurvey(answerSurveyDto);

        Assert.Contains(response, survey.Responses);
    }

    [Fact]
    public async Task ShouldSaveAnsweredSurveyInDatabase()
    {
        var template = TemplateBuilder.ATemplate().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);

        await _service.AnswerSurvey(answerSurveyDto);

        _surveyRepository.Verify(repository => repository.Update(survey));
    }

    [Fact]
    public async Task ShouldThrowNotFoundErrorIfSurveyNotFound()
    {
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);

        var notFoundException =
            await Assert.ThrowsAsync<NotFoundException>(async () => await _service.AnswerSurvey(answerSurveyDto));

        Assert.Equal("Survey not found", notFoundException.Message);
    }

    [Fact]
    public async Task ShouldThrowConnectionToDatabaseErrorIfSurveyCannotBeRetrieved()
    {
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ThrowsAsync(new Exception());

        var connectionToRepositoryUnsuccessfulException =
            await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () =>
                await _service.AnswerSurvey(answerSurveyDto));

        Assert.Equal("Unable to update answered survey", connectionToRepositoryUnsuccessfulException.Message);
    }

    [Fact]
    public async Task ShouldThrowSurveyAlreadyAnsweredExceptionIfSurveyIsAlreadyAnswered()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithResponses(new List<SurveyResponse>() { response })
            .Build();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);


        var surveyAlreadyAnsweredException = await Assert.ThrowsAsync<SurveyAlreadyAnsweredException>(async () =>
            await _service.AnswerSurvey(answerSurveyDto));

        Assert.Equal($"Survey already answered by {teamMember.Id}", surveyAlreadyAnsweredException.Message);
    }

    [Fact]
    public async Task ShouldThrowSurveyNotAssignedToTeamMemberExceptionIfTeamMemberIsNotOnTeamAssignedToSurvey()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var survey = SurveyBuilder.ASurvey().WithTeam(team).Build();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);


        var surveyNotAssignedToTeamMemberException =
            await Assert.ThrowsAsync<SurveyNotAssignedToTeamMemberException>(async () =>
                await _service.AnswerSurvey(answerSurveyDto));

        Assert.Equal($"{teamMember.Id} is not assigned to survey", surveyNotAssignedToTeamMemberException.Message);
    }

    [Fact]
    public async Task ShouldThrowSurveyNotCompleteExceptionIfTeamMemberHasNotAnsweredAllQuestions()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var team = TeamBuilder.ATeam().WithTeamMembers(new List<TeamMember>() { teamMember }).Build();
        IEnumerable<IQuestion> questions = new IQuestion[]
        {
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 1" },
                    { Languages.ENGLISH, "Question 1" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 1
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 2" },
                    { Languages.ENGLISH, "Question 2" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 2
            ),
            new HackmanQuestion(new Dictionary<string, string>()
                {
                    { Languages.SPANISH, "Pregunta 3" },
                    { Languages.ENGLISH, "Question 3" },
                }, new Dictionary<string, string[]>()
                {
                    { Languages.SPANISH, new[] { "1", "2", "3" } },
                    { Languages.ENGLISH, new[] { "1", "2", "3" } }
                }, false, HackmanComponent.INTERPERSONAL_PEER_COACHING, HackmanSubcategory.DELIMITED,
                HackmanSubcomponent.SENSE_OF_URGENCY, 3
            ),
        };
        var template = TemplateBuilder.ATemplate().WithQuestions(questions).Build();
        var survey = SurveyBuilder.ASurvey().WithTeam(team).WithTemplate(template).Build();
        var incompleteQuestionResponses = new List<QuestionResponse>()
        {
            new QuestionResponse(1, "1"),
            new QuestionResponse(2, "2"),
        };
        var surveyResponse = new SurveyResponse(teamMember.Id, incompleteQuestionResponses);
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, surveyResponse);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);

        await Assert.ThrowsAsync<SurveyNotCompleteException>(async () => await _service.AnswerSurvey(answerSurveyDto));
    }

    [Fact]
    public async Task ShouldGetSurveyFromRepositoryWhenCallingGetSurveyResponsesFile()
    {
        var surveyRepository = new Mock<ISurveyRepository>();
        var teamRepository = new Mock<ITeamRepository>();
        var templateRepository = new Mock<ITemplateRepository>();
        var surveyResponseCsvGenerator = new Mock<ISurveyResponsesFileCreate>();

        var surveyService =
            new SurveyService(surveyRepository.Object, teamRepository.Object, templateRepository.Object, surveyResponseCsvGenerator.Object);
        var surveyId = Guid.NewGuid();

        await surveyService.GetSurveyResponsesFile(surveyId);

        surveyRepository.Verify(repository => repository.GetSurveyById(surveyId));
    }

    [Fact]
    public async Task ShouldGetSurveyResponsesCsvFromFileGeneratorWhenCallingGetSurveyResponsesFile()
    {
        var surveyRepository = new Mock<ISurveyRepository>();
        var teamRepository = new Mock<ITeamRepository>();
        var templateRepository = new Mock<ITemplateRepository>();
        var surveyResponseCsvGenerator = new Mock<ISurveyResponsesFileCreate>();
        var surveyService =
            new SurveyService(surveyRepository.Object, teamRepository.Object, templateRepository.Object, surveyResponseCsvGenerator.Object);
        var surveyId = Guid.NewGuid();
        var survey = SurveyBuilder.ASurvey().WithId(surveyId).Build();

        surveyRepository.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(survey);
        await surveyService.GetSurveyResponsesFile(surveyId);
        surveyResponseCsvGenerator.Verify(generator => generator.CreateFile(survey));
    }

    [Fact]
    public async Task ShouldReturnSurveyResponsesCsvProvidedByFileGeneratorWhenCallingGetSurveyResponsesFile()
    {
        var surveyRepository = new Mock<ISurveyRepository>();
        var teamRepository = new Mock<ITeamRepository>();
        var templateRepository = new Mock<ITemplateRepository>();
        var surveyResponseCsvGenerator = new Mock<ISurveyResponsesFileCreate>();
        var surveyService =
            new SurveyService(surveyRepository.Object, teamRepository.Object, templateRepository.Object, surveyResponseCsvGenerator.Object);
        var surveyId = Guid.NewGuid();
        var survey = SurveyBuilder.ASurvey().WithId(surveyId).Build();
        
        var dummyCsvContent = "Id,Response\n1,Yes\n2,No";
        var dummyCsvBytes = Encoding.UTF8.GetBytes(dummyCsvContent);
        var memoryStream = new MemoryStream(dummyCsvBytes);
        
        var expectedSurveyResponsesFile = new FileStreamResult(memoryStream, "text/csv")
        {
            FileDownloadName = "survey_responses.csv"
        };

        surveyRepository.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(survey);
        surveyResponseCsvGenerator.Setup(generator => generator.CreateFile(survey)).Returns(expectedSurveyResponsesFile);

        var surveyResponsesFile = await surveyService.GetSurveyResponsesFile(surveyId);
        
        Assert.Equal(expectedSurveyResponsesFile, surveyResponsesFile);
    }
}