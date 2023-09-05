
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Model.Survey;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;
using Newtonsoft.Json;

namespace SherpaBackEnd.Tests.Controllers;

public class SurveyControllerTest
{
    private Mock<ISurveyService> _serviceMock;
    private ILogger<SurveyController> _logger = new Mock<ILogger<SurveyController>>().Object;

    public SurveyControllerTest()
    {
        _serviceMock = new Mock<ISurveyService>();
    }

    [Fact]
    public async Task ShouldCallServiceWithDtoFromBodyWhenCallingCreateSurveyAndReturn201()
    {
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), Guid.NewGuid(), "template-name", "title",
            "description", DateTime.Parse("2023-08-09T07:38:04+0000"));
        var controller = new SurveyController(_serviceMock.Object, _logger);

        var actionResult = await controller.CreateSurvey(createSurveyDto);

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
        var controller = new SurveyController(_serviceMock.Object, _logger);

        _serviceMock.Setup(service => service.CreateSurvey(It.IsAny<CreateSurveyDto>())).ThrowsAsync(new Exception());

        var actionResult = await controller.CreateSurvey(createSurveyDto);


        var createdResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, createdResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400StatusCodeIfServiceThrowsNotFoundExceptionWhenCallingCreateSurvey()
    {
        var createSurveyDto = new CreateSurveyDto(Guid.NewGuid(), Guid.NewGuid(), "template-name", "title",
            "description", DateTime.Parse("2023-08-09T07:38:04+0000"));
        var controller = new SurveyController(_serviceMock.Object, _logger);

        const string exceptionMessage = "test";
        _serviceMock.Setup(service => service.CreateSurvey(It.IsAny<CreateSurveyDto>()))
            .ThrowsAsync(new NotFoundException(exceptionMessage));

        var actionResult = await controller.CreateSurvey(createSurveyDto);


        var createdResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, createdResult.StatusCode);
        Assert.Equal(exceptionMessage, createdResult.Value);
    }

    [Fact]
    public async Task ShouldReturnTheSurveyGivenByTheServiceWhenCallingGetSurveyById()
    {
        var expectedSurvey = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", new List<Response>(),
            new Team(Guid.NewGuid(), "team name"), new Template("Template name", Array.Empty<IQuestion>(), 1));
        var controller = new SurveyController(_serviceMock.Object, _logger);
        _serviceMock.Setup(service => service.GetSurveyById(expectedSurvey.Id)).ReturnsAsync(expectedSurvey);

        var surveyById = await controller.GetSurveyById(expectedSurvey.Id);

        var okObjectResult = Assert.IsType<OkObjectResult>(surveyById.Result);

        Assert.Equal(expectedSurvey, okObjectResult.Value);
    }

    [Fact]
    public async Task ShouldReturn404IfServiceThrowsNotFoundExceptionWhenCallingGetSurveyById()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyById(surveyId))
            .ThrowsAsync(new NotFoundException("Survey not found"));

        var controller = new SurveyController(_serviceMock.Object, _logger);
        var surveyById = await controller.GetSurveyById(surveyId);

        var okObjectResult = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status404NotFound, okObjectResult.StatusCode);
    }
    [Fact]
    public async Task ShouldReturn500IfServiceThrowsNotPredefinedCustomExceptionWhenCallingGetSurveyById()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyById(surveyId))
            .ThrowsAsync(new Exception());

        var controller = new SurveyController(_serviceMock.Object, _logger);
        var surveyById = await controller.GetSurveyById(surveyId);

        var okObjectResult = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status500InternalServerError, okObjectResult.StatusCode);
    }
    [Fact]
    public async Task ShouldReturnOkObjectResultIfNoErrorsFoundWhenGettingAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
    }

    [Fact]
    public async Task ShouldReturnAListOfSurveysWhenCalledGetAllSurveysFromTeam()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>();
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        Assert.IsAssignableFrom<IEnumerable<Survey>>(responseObject.Value);
    }

    [Fact]
    public async Task ShouldSerializeCorrectlyListOfSurveysWhenCallingGetAllSurveysFromTeam()
    {
        var surveysEmptyList = new List<Survey>();
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);

        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId)).ReturnsAsync(surveysEmptyList);
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);
        
        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);
        
        surveyService.Verify(_ => _.GetAllSurveysFromTeam(teamId), Times.Once());
        var responseObject = Assert.IsType<OkObjectResult>(allSurveysFromTeam.Result);
        var actualSurveys = Assert.IsAssignableFrom<IEnumerable<Survey>>(responseObject.Value);
        
        Assert.Equal(JsonConvert.SerializeObject(surveysEmptyList), JsonConvert.SerializeObject(actualSurveys));
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfAllSurveysFromTeamCannotBeRetrieved()
    {
        var teamId = Guid.NewGuid();
        var surveyService = new Mock<ISurveyService>(MockBehavior.Strict);
        var notSuccessfulGettingAllSurveysFromTeam = new ConnectionToRepositoryUnsuccessfulException("Cannot perform get all teams function.");
        surveyService.Setup(_ => _.GetAllSurveysFromTeam(teamId))
            .ThrowsAsync(notSuccessfulGettingAllSurveysFromTeam);
        
        var logger = Mock.Of<ILogger<TeamController>>();
        var surveyController = new SurveyController(surveyService.Object, _logger);

        var allSurveysFromTeam = await surveyController.GetAllSurveysFromTeam(teamId);

        var resultObject = Assert.IsType<ObjectResult>(allSurveysFromTeam.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }
}