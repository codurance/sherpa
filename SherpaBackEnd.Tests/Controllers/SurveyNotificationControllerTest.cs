using JetBrains.Annotations;
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
        var launchSurveyDto = new CreateSurveyNotificationsDto(Guid.NewGuid());

        var controller = new SurveyNotificationController(surveyNotificationService.Object);

        await controller.LaunchSurvey(launchSurveyDto);
        
        surveyNotificationService.Verify(service => service.CreateNotificationsFor(launchSurveyDto.SurveyId));
    }
}