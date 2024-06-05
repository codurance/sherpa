using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
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
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);

        await _service.AnswerSurvey(answerSurveyDto);

        _surveyRepository.Verify(repository => repository.GetSurveyById(survey.Id));
    }
    
    [Fact]
    public async Task ShouldAnswerSurveyWithResponseInDto()
    {
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        _surveyRepository.Setup(repository => repository.GetSurveyById(survey.Id)).ReturnsAsync(survey);

        await _service.AnswerSurvey(answerSurveyDto);

        Assert.Contains(response, survey.Responses);
    }
}