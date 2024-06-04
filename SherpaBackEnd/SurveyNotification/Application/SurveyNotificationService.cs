using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Application;

public class SurveyNotificationService : ISurveyNotificationService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyNotificationsRepository _surveyNotificationsRepository;

    public SurveyNotificationService(ISurveyRepository surveyRepository,
        ISurveyNotificationsRepository surveyNotificationsRepository, EmailTemplateFactory emailTemplateFactory)
    {
        _surveyRepository = surveyRepository;
        _surveyNotificationsRepository = surveyNotificationsRepository;
    }

    public async Task LaunchSurveyNotificationsFor(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        var survey = await _surveyRepository.GetSurveyById(createSurveyNotificationsDto.SurveyId);
        var surveyNotifications = new List<Domain.SurveyNotification>();

        if (survey != null)
        {
            surveyNotifications.AddRange(survey.Team.Members.Select(teamMember =>
                new Domain.SurveyNotification(GenerateId(), survey, teamMember)));
            
            await _surveyNotificationsRepository.CreateManySurveyNotification(surveyNotifications);
        }
    }

    protected virtual Guid GenerateId()
    {
        return Guid.NewGuid();
    }
}