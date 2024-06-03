using JetBrains.Annotations;
using Moq;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Application;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.SurveyNotification.Application;

[TestSubject(typeof(SurveyNotificationService))]
public class SurveyNotificationServiceTest
{

    [Fact]
    public async Task ShouldRetrieveTheSurveyWithTheSurveyIdInTheDto()
    {
        var surveyRepository = new Mock<ISurveyRepository>();
        var surveyNotificationService = new SurveyNotificationService(surveyRepository.Object);

        var createSurveyNotificationsDto = new CreateSurveyNotificationsDto(Guid.NewGuid());
        await surveyNotificationService.LaunchSurveyNotificationsFor(createSurveyNotificationsDto);
        
        surveyRepository.Verify(repository => repository.GetSurveyById(createSurveyNotificationsDto.SurveyId));
    }
}