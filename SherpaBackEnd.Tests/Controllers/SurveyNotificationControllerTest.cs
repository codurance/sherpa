using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Controllers;

[TestSubject(typeof(SurveyNotificationController))]
public class SurveyNotificationControllerTest
{
    private Mock<ISurveyNotificationService> _surveyNotificationService;
    private Mock<ILogger<SurveyNotificationController>> _logger;
    private SurveyNotificationController _surveyNotificationController;
    private readonly CreateSurveyNotificationsDto _createSurveyNotificationsDto = new CreateSurveyNotificationsDto(Guid.NewGuid());

    public SurveyNotificationControllerTest()
    {
        
        _surveyNotificationService = new Mock<ISurveyNotificationService>();
        _logger = new Mock<ILogger<SurveyNotificationController>>();
        _surveyNotificationController = new SurveyNotificationController(_surveyNotificationService.Object, _logger.Object);
    }

    [Fact]
    public async Task ShouldCreateNotificationsWithSurveyIdWithDtoFromBody()
    {
        var actionResult = await _surveyNotificationController.LaunchSurvey(_createSurveyNotificationsDto);
        
        _surveyNotificationService.Verify(service => service.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));

        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnBadRequestIfServiceThrowsNotFoundExceptionWhenCallingLaunchSurvey()
    {
        var surveyId = Guid.NewGuid();

        var notFoundException = new NotFoundException("Survey not found");
        _surveyNotificationService.Setup(service => service.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto))
            .ThrowsAsync(notFoundException);

        var actionResult = await _surveyNotificationController.LaunchSurvey(_createSurveyNotificationsDto);

        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        
        Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        Assert.Equal(notFoundException.Message, objectResult.Value);
        
    }
    
    [Fact]
    public async Task ShouldReturnInternalServerErrorIfServiceThrowsExceptionWhenCallingLaunchSurvey()
    {
        var notFoundException = new Exception();
        _surveyNotificationService.Setup(service => service.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto))
            .ThrowsAsync(notFoundException);

        var actionResult = await _surveyNotificationController.LaunchSurvey(_createSurveyNotificationsDto);

        var objectResult = Assert.IsType<ObjectResult>(actionResult);
        
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.Equal(notFoundException.Message, objectResult.Value);
    }
}