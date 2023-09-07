using Moq;
using Shared.Test.Helpers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;
using Xunit.Abstractions;
using ISurveyRepository = SherpaBackEnd.Model.ISurveyRepository;

namespace SherpaBackEnd.Tests.Services;

public class SurveyServiceTest
{
    private Mock<ISurveyRepository> _surveyRepo;
    private Mock<ITeamRepository> _teamRepo;
    private Mock<ITemplateRepository> _templateRepo;
    private readonly ITestOutputHelper output;

    public SurveyServiceTest(ITestOutputHelper output)
    {
        this.output = output;
        _surveyRepo = new Mock<ISurveyRepository>();
        _teamRepo = new Mock<ITeamRepository>();
        _templateRepo = new Mock<ITemplateRepository>();
    }

    [Fact]
    public async Task ShouldRetrieveTemplateAndTeamFromTheirReposAndPersistSurveyOnSurveyRepoWhenCallingCreateSurvey()
    {
        var team = new Team(Guid.NewGuid(), "Demo team");
        var template = new Template("demo", Array.Empty<IQuestion>(), 30);

        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);

        _teamRepo.Setup(repository => repository.GetTeamByIdAsync(team.Id)).ReturnsAsync(team);
        _templateRepo.Setup(repository => repository.GetTemplateByName(template.Name)).ReturnsAsync(template);

        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), team.Id, template.Name, "Title", "Description",
            DateTime.Parse("2023-08-09T07:38:04+0000"));
        await service.CreateSurvey(createSurveyDto);

        var expectedSurvey = new Survey(createSurveyDto.SurveyId, new User(service.DefaultUserId, "Lucia"),
            Status.Draft, createSurveyDto.Deadline, createSurveyDto.Title, createSurveyDto.Description,
            new List<Response>(), team, template);
        var surveyRepoInvocation = _surveyRepo.Invocations[0];
        var actualSurvey = surveyRepoInvocation.Arguments[0];
        CustomAssertions.StringifyEquals(expectedSurvey, actualSurvey);
    }

    [Fact]
    public async Task ShouldReturnSurveyGivenByTheRepository()
    {
        var surveyId = Guid.NewGuid();
        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);
        var expectedSurvey = new Survey(Guid.NewGuid(), new User(service.DefaultUserId, "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "Description", new List<Response>(),
            new Team(Guid.NewGuid(), "Demo team"), new Template("demo", Array.Empty<IQuestion>(), 30));
        _surveyRepo.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(expectedSurvey);

        var receivedSurvey = await service.GetSurveyById(surveyId);

        _surveyRepo.Verify(repository => repository.GetSurveyById(surveyId));
        CustomAssertions.StringifyEquals(expectedSurvey, receivedSurvey);
    }

    [Fact]
    public async Task ShouldThrowNotFoundExceptionIfRepoReturnsNull()
    {
        var surveyId = Guid.NewGuid();
        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);
        _surveyRepo.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync((Survey?)null);


        await Assert.ThrowsAsync<NotFoundException>(async () => await service.GetSurveyById(surveyId));
    }

    [Fact]
    public async Task ShouldCallSurveyRepositoryToGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);

        await service.GetAllSurveysFromTeam(teamId);

        _surveyRepo.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowErrorIfConnectionToRepositoryNotSuccessful()
    {
        var teamId = Guid.NewGuid();
        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);

        _surveyRepo.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ThrowsAsync(new Exception());

        var exceptionThrown =
            await Assert.ThrowsAsync<ConnectionToRepositoryUnsuccessfulException>(async () =>
                await service.GetAllSurveysFromTeam(teamId));
        Assert.IsType<ConnectionToRepositoryUnsuccessfulException>(exceptionThrown);
    }

    [Fact]
    public async Task ShouldCallGetSurveyById()
    {
        var surveyId = Guid.NewGuid();
        var service = new SurveyService(_surveyRepo.Object, _teamRepo.Object, _templateRepo.Object);
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
        var expectedSurvey = new Survey(Guid.NewGuid(), new User(service.DefaultUserId, "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "Description", new List<Response>(),
            new Team(Guid.NewGuid(), "Demo team"), new Template("demo", new List<IQuestion>() { hackmanQuestion }, 30));
        _surveyRepo.Setup(repository => repository.GetSurveyById(surveyId)).ReturnsAsync(expectedSurvey);

        var questionsReceived = await service.GetSurveyQuestionsBySurveyId(surveyId);

        _surveyRepo.Verify(repository => repository.GetSurveyById(surveyId));

        Assert.Contains(hackmanQuestion, questionsReceived);
    }
}