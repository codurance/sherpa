namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailTemplateFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public EmailTemplate CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications)
    {
        //TODO: Revisit this
        string body = surveyNotifications.Select(notification => notification.Survey.Description).First().ToString() ?? string.Empty;
        var recipients = surveyNotifications.Select(notification =>
            new Recipient(notification.TeamMember.Email, CreateAnswerSurveyUrl(notification))).ToList();
        return new EmailTemplate(body, recipients);
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        return $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}