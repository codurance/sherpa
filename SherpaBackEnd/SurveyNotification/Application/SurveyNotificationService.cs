using System.Runtime.InteropServices;
using SherpaBackEnd.Email;
using SherpaBackEnd.Email.Application;
using SherpaBackEnd.Email.Templates.NewSurvey;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Shared.Services;
using SherpaBackEnd.Survey.Domain.Persistence;
using SherpaBackEnd.SurveyNotification.Infrastructure.Http.Dto;
using SherpaBackEnd.SurveyNotification.Infrastructure.Persistence;

namespace SherpaBackEnd.SurveyNotification.Application;

public class SurveyNotificationService : ISurveyNotificationService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyNotificationsRepository _surveyNotificationsRepository;
    private readonly IEmailTemplateFactory _emailTemplateFactory;
    private readonly IGuidService? _guidService;
    private readonly IEmailService _emailService;

    public SurveyNotificationService(ISurveyRepository surveyRepository,
        ISurveyNotificationsRepository surveyNotificationsRepository, IEmailTemplateFactory emailTemplateFactory,
        IEmailService emailService, [Optional] IGuidService? guidService)
    {
        _surveyRepository = surveyRepository;
        _surveyNotificationsRepository = surveyNotificationsRepository;
        _emailTemplateFactory = emailTemplateFactory;
        _guidService = guidService;
        _emailService = emailService;
    }

    public async Task LaunchSurveyNotificationsFor(CreateSurveyNotificationsDto createSurveyNotificationsDto)
    {
        var survey = await GetSurveyById(createSurveyNotificationsDto.SurveyId);

        var surveyNotifications = new List<Domain.SurveyNotification>();

        surveyNotifications.AddRange(survey.Team.Members.Select(teamMember =>
            new Domain.SurveyNotification(GenerateId(), survey, teamMember)));

        await CreateManySurveyNotification(surveyNotifications);

        var newSurveyEmailTemplateDto = new NewSurveyEmailTemplateDto(surveyNotifications);
        var emailTemplateRequest = _emailTemplateFactory.CreateEmailTemplate(newSurveyEmailTemplateDto);
        await SendEmailsWith(emailTemplateRequest);
    }

    private async Task SendEmailsWith(EmailTemplate emailTemplateRequest)
    {
        try
        {
            await _emailService.SendEmailsWith(emailTemplateRequest);
        }
        catch (Exception e)
        {
            throw new EmailSendingException("Unable to send email with external service", e);
        }
    }

    public async Task<Domain.SurveyNotification> GetSurveyNotification(Guid surveyNotificationId)
    {
        return await _surveyNotificationsRepository.GetSurveyNotificationById(surveyNotificationId);
    }

    private async Task CreateManySurveyNotification(List<Domain.SurveyNotification> surveyNotifications)
    {
        try
        {
            await _surveyNotificationsRepository.CreateManySurveyNotification(surveyNotifications);
        }
        catch (Exception e)
        {
            throw new ConnectionToRepositoryUnsuccessfulException("Unable to create Survey notifications in database",
                e);
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

    // TODO remove method by implementing _guidService in tests
    protected virtual Guid GenerateId()
    {
        return _guidService?.GenerateRandomGuid() ?? Guid.NewGuid();
    }
}