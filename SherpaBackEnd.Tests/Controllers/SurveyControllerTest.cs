using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Exceptions;
using Newtonsoft.Json;
using SherpaBackEnd.Survey.Application;
using SherpaBackEnd.Survey.Domain;
using SherpaBackEnd.Survey.Domain.Exceptions;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.Survey.Infrastructure.Http.Dto;
using SherpaBackEnd.Team.Infrastructure.Http;
using SherpaBackEnd.Template.Domain;
using SherpaBackEnd.Template.Infrastructure.Http.Dto;
using SherpaBackEnd.Tests.Builders;

namespace SherpaBackEnd.Tests.Controllers;

public class SurveyControllerTest
{
    private Mock<ISurveyService> _serviceMock;
    private ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;
    private SurveyController _controller;

    public SurveyControllerTest()
    {
        _serviceMock = new Mock<ISurveyService>();
        _controller = new SurveyController(_serviceMock.Object, _logger);
    }

    [Fact]
    public async Task ShouldCallServiceWithDtoFromBodyWhenCallingCreateSurveyAndReturn201()
    {
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), Guid.NewGuid(), "template-name", "title",
            "description", DateTime.Parse("2023-08-09T07:38:04+0000"));

        var actionResult = await _controller.CreateSurvey(createSurveyDto);

        _serviceMock.Verify(service =>
            service.CreateSurvey(createSurveyDto)
        );

        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500StatusCodeIfServiceThrowsWhenCallingCreateSurvey()
    {
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), Guid.NewGuid(), "template-name", "title",
            "description", DateTime.Parse("2023-08-09T07:38:04+0000"));
        _serviceMock.Setup(service => service.CreateSurvey(It.IsAny<CreateSurveyDto>())).ThrowsAsync(new Exception());

        var actionResult = await _controller.CreateSurvey(createSurveyDto);


        var createdResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, createdResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400StatusCodeIfServiceThrowsNotFoundExceptionWhenCallingCreateSurvey()
    {
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), Guid.NewGuid(), "template-name", "title",
            "description", DateTime.Parse("2023-08-09T07:38:04+0000"));
        const string exceptionMessage = "test";
        _serviceMock.Setup(service => service.CreateSurvey(It.IsAny<CreateSurveyDto>()))
            .ThrowsAsync(new NotFoundException(exceptionMessage));

        var actionResult = await _controller.CreateSurvey(createSurveyDto);


        var createdResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, createdResult.StatusCode);
        Assert.Equal(exceptionMessage, createdResult.Value);
    }

    [Fact]
    public async Task ShouldReturnTheSurveyWithoutQuestionsGivenByTheServiceWhenCallingGetSurveyById()
    {
        var expectedSurvey = new SurveyWithoutQuestions(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Lucia"),
            SurveyStatus.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", new List<SurveyResponse>(),
            new Team.Domain.Team(Guid.NewGuid(), "team name"), new TemplateWithoutQuestions("Template name", 1));
        _serviceMock.Setup(service => service.GetSurveyWithoutQuestionsById(expectedSurvey.Id))
            .ReturnsAsync(expectedSurvey);

        var surveyById = await _controller.GetSurveyWithoutQuestionsById(expectedSurvey.Id);

        var okObjectResult = Assert.IsType<OkObjectResult>(surveyById.Result);

        Assert.Equal(expectedSurvey, okObjectResult.Value);
    }

    [Fact]
    public async Task ShouldReturnTheSurveyQuestionsGivenByTheSurveyServiceWhenCallingGetSurveyQuestionsBySurveyId()
    {
        var team = new Team.Domain.Team(Guid.NewGuid(), "team name");

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

        var template = new Template.Domain.Template("Template name", new List<IQuestion>() { hackmanQuestion }, 1);
        var expectedSurvey = new Survey.Domain.Survey(Guid.NewGuid(), new User.Domain.User(Guid.NewGuid(), "Lucia"),
            SurveyStatus.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", new List<SurveyResponse>(),
            team, template);
        _serviceMock.Setup(service => service.GetSurveyQuestionsBySurveyId(expectedSurvey.Id))
            .ReturnsAsync(expectedSurvey.Template.Questions);

        var questionsFromSurvey = await _controller.GetSurveyQuestionsBySurveyId(expectedSurvey.Id);

        _serviceMock.Verify(_ => _.GetSurveyQuestionsBySurveyId(expectedSurvey.Id));

        var okObjectResult = Assert.IsType<OkObjectResult>(questionsFromSurvey.Result);

        Assert.Contains(hackmanQuestion, (IEnumerable<IQuestion>)okObjectResult.Value);
    }

    [Fact]
    public async Task ShouldReturn404IfServiceThrowsNotFoundExceptionWhenCallingGetSurveyById()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyWithoutQuestionsById(surveyId))
            .ThrowsAsync(new NotFoundException("Survey not found"));

        var surveyById = await _controller.GetSurveyWithoutQuestionsById(surveyId);

        var okObjectResult = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status404NotFound, okObjectResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500IfServiceThrowsNotPredefinedCustomExceptionWhenCallingGetSurveyById()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyWithoutQuestionsById(surveyId))
            .ThrowsAsync(new Exception());

        var surveyById = await _controller.GetSurveyWithoutQuestionsById(surveyId);

        var okObjectResult = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status500InternalServerError, okObjectResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnOkObjectResultIfNoErrorsFoundWhenGettingAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();

        var logger = Mock.Of<ILogger<TeamController>>();

        var allSurveysFromTeam = await _controller.GetAllSurveysFromTeam(teamId);
        Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
    }

    [Fact]
    public async Task ShouldReturnAListOfSurveysWhenCalledGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();

        var logger = Mock.Of<ILogger<TeamController>>();

        var allSurveysFromTeam = await _controller.GetAllSurveysFromTeam(teamId);
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        Assert.IsAssignableFrom<IEnumerable<Survey.Domain.Survey>>(responseObject.Value);
    }

    [Fact]
    public async Task ShouldSerializeCorrectlyListOfSurveysWhenCallingGetAllSurveysFromTeam()
    {
        var surveysEmptyList = new List<Survey.Domain.Survey>();
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);

        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ReturnsAsync(surveysEmptyList);

        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);

        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);

        surveyService.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once());
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        var actualSurveys = Assert.IsAssignableFrom<IEnumerable<Survey.Domain.Survey>>(responseObject.Value);

        Assert.Equal(JsonConvert.SerializeObject(surveysEmptyList), JsonConvert.SerializeObject(actualSurveys));
    }

    [Fact]
    public async Task ShouldReturnErrorIfAllSurveysFromTeamCannotBeRetrieved()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);
        var notSuccessfulGettingAllSurveysFromTeam =
            new ConnectionToRepositoryUnsuccessfulException("Cannot perform get all teams function.");
        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId))
            .ThrowsAsync(notSuccessfulGettingAllSurveysFromTeam);

        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);

        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<ObjectResult>(allSurveysFromTeam.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }


    [Fact]
    public async Task ShouldCallServiceWithDtoFromBodyWhenCallingAnswerSurveyAndReturningCreatedResult()
    {
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);

        var actionResult = await _controller.AnswerSurvey(answerSurveyDto);
        Assert.IsType<CreatedResult>(actionResult);

        _serviceMock.Verify(service => service.AnswerSurvey(answerSurveyDto));
    }


    [Fact]
    public async Task ShouldReturnErrorIfSurveyResponseCouldNotBeCreated()
    {
        var survey = SurveyBuilder.ASurvey().Build();
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        var connectionToRepositoryUnsuccessfulException =
            new ConnectionToRepositoryUnsuccessfulException("Cannot create survey response.");
        _serviceMock.Setup(service => service.AnswerSurvey(answerSurveyDto))
            .ThrowsAsync(connectionToRepositoryUnsuccessfulException);

        var actionResult = (ObjectResult)await _controller.AnswerSurvey(answerSurveyDto);
        Assert.IsType<ObjectResult>(actionResult);

        Assert.Equal(StatusCodes.Status500InternalServerError, actionResult.StatusCode);
        Assert.Equal(connectionToRepositoryUnsuccessfulException.Message, actionResult.Value);
    }

    [Fact]
    public async Task ShouldReturnBadRequestIfSurveyHasAlreadyBeenAnsweredByTeamMembers()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var survey = SurveyBuilder.ASurvey().Build();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        var surveyAlreadyAnsweredException =
            new SurveyAlreadyAnsweredException(teamMember.Id);
        _serviceMock.Setup(service => service.AnswerSurvey(answerSurveyDto))
            .ThrowsAsync(surveyAlreadyAnsweredException);

        var actionResult = (ObjectResult)await _controller.AnswerSurvey(answerSurveyDto);
        Assert.IsType<ObjectResult>(actionResult);

        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        Assert.Equal(surveyAlreadyAnsweredException.Message, actionResult.Value);
    }

    [Fact]
    public async Task ShouldReturnBadRequestIfTeamMemberIsNotAssignedToSurvey()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var survey = SurveyBuilder.ASurvey().Build();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        var surveyNotAssignedToTeamMemberException =
            new SurveyNotAssignedToTeamMemberException(teamMember.Id);
        _serviceMock.Setup(service => service.AnswerSurvey(answerSurveyDto))
            .ThrowsAsync(surveyNotAssignedToTeamMemberException);

        var actionResult = (ObjectResult)await _controller.AnswerSurvey(answerSurveyDto);
        Assert.IsType<ObjectResult>(actionResult);

        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
        Assert.Equal(surveyNotAssignedToTeamMemberException.Message, actionResult.Value);
    }

    [Fact]
    public async Task ShouldReturnBadRequestIfTeamMemberDoesNotAnswerAllQuestionsInSurvey()
    {
        var teamMember = TeamMemberBuilder.ATeamMember().Build();
        var response = new SurveyResponse(teamMember.Id, new List<QuestionResponse>());
        var survey = SurveyBuilder.ASurvey().Build();
        var answerSurveyDto = new AnswerSurveyDto(survey.Id, teamMember.Id, response);
        var surveyNotCompleteException = new SurveyNotCompleteException();
        _serviceMock.Setup(service => service.AnswerSurvey(answerSurveyDto)).ThrowsAsync(surveyNotCompleteException);

        var actionResult = await _controller.AnswerSurvey(answerSurveyDto);

        var objectResult = Assert.IsType<ObjectResult>(actionResult);               

        Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        Assert.Equal(surveyNotCompleteException.Message, objectResult.Value);
    }
}