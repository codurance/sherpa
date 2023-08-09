using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Dtos;
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
}