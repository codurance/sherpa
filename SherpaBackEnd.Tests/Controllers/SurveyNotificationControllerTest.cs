using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Controllers;

[TestSubject(typeof(SurveyNotificationController))]
public class SurveyNotificationControllerTest
{
    private Mock<ISurveyNotificationService> _surveyNotificationService;
    private SurveyNotificationController _surveyNotificationController;
    private readonly CreateSurveyNotificationsDto _createSurveyNotificationsDto = new CreateSurveyNotificationsDto(Guid.NewGuid());

    public SurveyNotificationControllerTest()
    {
        _surveyNotificationService = new Mock<ISurveyNotificationService>();
        _surveyNotificationController = new SurveyNotificationController(_surveyNotificationService.Object);
    }

    [Fact]
    public async Task ShouldCreateNotificationsWithSurveyIdWithDtoFromBody()
    {
        var actionResult = await _surveyNotificationController.LaunchSurvey(_createSurveyNotificationsDto);
        
        _surveyNotificationService.Verify(service => service.LaunchSurveyNotificationsFor(_createSurveyNotificationsDto));

        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
    }
}