using SherpaBackEnd.Email.Templates;
using SherpaBackEnd.Email.Templates.NewSurvey;

namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailTemplateFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public EmailTemplate CreateEmailTemplate(CreateEmailTemplateDto createEmailTemplateDto)
    {
        return createEmailTemplateDto switch
        {
            NewSurveyEmailTemplateDto newSurveyEmailTemplateDto => CreateNewSurveyEmailTemplate(
                newSurveyEmailTemplateDto),
            _ => throw new NotImplementedException()
        };
    }

    private EmailTemplate CreateNewSurveyEmailTemplate(NewSurveyEmailTemplateDto newSurveyEmailTemplateDto)
    {
        var recipients = newSurveyEmailTemplateDto.SurveyNotifications.Select(notification =>
        {
            var newSurveyTemplateModel = CreateNewSurveyTemplateModel(notification);
            var html = newSurveyTemplateModel.CreateHtmlBody();
            var text = newSurveyTemplateModel.CreateTextBody();
            
            return new Recipient(notification.TeamMember.FullName, notification.TeamMember.Email,
                CreateAnswerSurveyUrl(notification))
            {
                HtmlBody = html,
                TextBody = text,
            };
        }).ToList();
        return new EmailTemplate("NewSurvey", recipients);
    }

    private NewSurveyTemplateModel CreateNewSurveyTemplateModel(SurveyNotification.Domain.SurveyNotification notification)
    {
        return new NewSurveyTemplateModel()
        {
            Url = CreateAnswerSurveyUrl(notification),
            Deadline = notification.Survey.Deadline?.ToString("dd MMMM yyyy"),
            SurveyName = notification.Survey.Title,
            Name = notification.TeamMember.FullName
                
        };
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        // var scheme = httpContextRequest?.Scheme;
        var scheme = "https"; // TODO: don't hardcode scheme after enabling SSL
        return $"{scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}