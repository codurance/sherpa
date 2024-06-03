using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.SurveyNotification;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Controllers;

[TestSubject(typeof(SurveyNotificationController))]
public class SurveyNotificationControllerTest
{
    [Fact]
    public async Task ShouldCreateNotificationsWithSurveyIdWithDtoFromBody()
    {
        var surveyNotificationService = new Mock<ISurveyNotificationService>();
        var createSurveyNotificationDto = new CreateSurveyNotificationsDto(Guid.NewGuid());

        var controller = new SurveyNotificationController(surveyNotificationService.Object);

        var actionResult = await controller.LaunchSurvey(createSurveyNotificationDto);
        
        surveyNotificationService.Verify(service => service.CreateNotificationsFor(createSurveyNotificationDto));

        var createdResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);

    }
}