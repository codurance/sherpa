using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Application;

public class SurveyNotificationService : ISurveyNotificationService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyNotificationsRepository _surveyNotificationsRepository;

    public SurveyNotificationService(ISurveyRepository surveyRepository,
        ISurveyNotificationsRepository surveyNotificationsRepository)
    {
        _surveyRepository = surveyRepository;
        _surveyNotificationsRepository = surveyNotificationsRepository;
    }

    public async Task LaunchSurveyNotificationsFor(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        var survey = await _surveyRepository.GetSurveyById(createSurveyNotificationsDto.SurveyId);

        if (survey != null)
            foreach (var surveyNotification in survey.Team.Members.Select(teamMember =>
                         new Domain.SurveyNotification(GenerateId(), survey.Id, teamMember.Id)))
            {
                await _surveyNotificationsRepository.CreateSurveyNotification(surveyNotification);
            }
    }

    protected virtual Guid GenerateId()
    {
        return Guid.NewGuid();
    }
}