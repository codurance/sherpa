using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Application;

public class SurveyNotificationService : ISurveyNotificationService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyNotificationsRepository _surveyNotificationsRepository;
    private readonly IEmailTemplateFactory _emailTemplateFactory;

    public SurveyNotificationService(ISurveyRepository surveyRepository,
        ISurveyNotificationsRepository surveyNotificationsRepository, IEmailTemplateFactory emailTemplateFactory)
    {
        _surveyRepository = surveyRepository;
        _surveyNotificationsRepository = surveyNotificationsRepository;
        _emailTemplateFactory = emailTemplateFactory;
    }

    public async Task LaunchSurveyNotificationsFor(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        var survey = await GetSurveyById(createSurveyNotificationsDto.SurveyId);

        var surveyNotifications = new List<Domain.SurveyNotification>();

        if (survey != null)
        {
            surveyNotifications.AddRange(survey.Team.Members.Select(teamMember =>
                new Domain.SurveyNotification(GenerateId(), survey, teamMember)));
            
            await _surveyNotificationsRepository.CreateManySurveyNotification(surveyNotifications);
            
            _emailTemplateFactory.CreateEmailTemplate(surveyNotifications);
        }
    }

    private async Task<Survey.Domain.Survey> GetSurveyById(Guid surveyId)
    {
        Survey.Domain.Survey survey;
        try
        {
            survey = await _surveyRepository.GetSurveyById(surveyId);
        }
        catch (Exception e)
        {
            throw new ConnectionToRepositoryUnsuccessfulException("Unable to retrieve Survey from database", e);
        }

        if (survey == null)
        {
            throw new NotFoundException("Survey not found");
        }

        return survey;
    }

    protected virtual Guid GenerateId()
    {
        return Guid.NewGuid();
    }
}