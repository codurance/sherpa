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
    public async Task ShouldReturn500StatusCodeIfServiceThrows()
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
    public async Task ShouldReturn400StatusCodeIfServiceThrowsNotFoundException()
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
    public async Task ShouldReturnTheSurveyGivenByTheService()
    {
        var expectedSurvey = new Survey(Guid.NewGuid(), new User(Guid.NewGuid(), "Lucia"), Status.Draft,
            DateTime.Parse("2023-08-09T07:38:04+0000"), "Title", "description", Array.Empty<Response>(),
            new Team(Guid.NewGuid(), "team name"), new Template("Template name", Array.Empty<IQuestion>(), 1));
        var controller = new SurveyController(_serviceMock.Object, _logger);
        _serviceMock.Setup(service => service.GetSurveyById(expectedSurvey.Id)).ReturnsAsync(expectedSurvey);

        var surveyById = await controller.GetSurveyById(expectedSurvey.Id);

        var okObjectResul = Assert.IsType<OkObjectResult>(surveyById.Result);

        Assert.Equal(expectedSurvey, okObjectResul.Value);
    }

    [Fact]
    public async Task ShouldReturn404IfServiceThrowsNotFoundException()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyById(surveyId))
            .ThrowsAsync(new NotFoundException("Survey not found"));

        var controller = new SurveyController(_serviceMock.Object, _logger);
        var surveyById = await controller.GetSurveyById(surveyId);

        var okObjectResul = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status404NotFound, okObjectResul.StatusCode);
    }
    [Fact]
    public async Task ShouldReturn500IfServiceThrowsNotPredefinedCustomException()
    {
        var surveyId = Guid.NewGuid();

        _serviceMock.Setup(service => service.GetSurveyById(surveyId))
            .ThrowsAsync(new Exception());

        var controller = new SurveyController(_serviceMock.Object, _logger);
        var surveyById = await controller.GetSurveyById(surveyId);

        var okObjectResul = Assert.IsType<ObjectResult>(surveyById.Result);

        Assert.Equal(StatusCodes.Status500InternalServerError, okObjectResul.StatusCode);
    }
}