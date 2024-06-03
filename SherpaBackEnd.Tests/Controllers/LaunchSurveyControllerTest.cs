using JetBrains.Annotations;
using Moq;
using SherpaBackEnd.Sur;
using SherpaBackEnd.Survey.Infrastructure.Http;
using SherpaBackEnd.SurveyNotification.Application;

namespace SherpaBackEnd.Tests.Controllers;

[TestSubject(typeof(LaunchSurveyController))]
public class LaunchSurveyControllerTest
{
    [Fact]
    public async Task ShouldCreateNotificationsWithSurveyIdWithDtoFromBody()
    {
        var surveyNotificationService = new Mock<ISurveyNotificationService>();
        var launchSurveyDto = new LaunchSurveyDto(Guid.NewGuid());

        var controller = new LaunchSurveyController(surveyNotificationService.Object);

        await controller.LaunchSurvey(launchSurveyDto);
        
        surveyNotificationService.Verify(service => service.CreateNotificationsFor(launchSurveyDto.SurveyId));
    }
}