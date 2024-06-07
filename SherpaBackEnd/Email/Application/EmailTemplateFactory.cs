namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailTemplateFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<EmailTemplate> CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications)
    {
        return surveyNotifications.Select(notification =>
            new EmailTemplate(notification.TeamMember.Email, CreateAnswerSurveyUrl(notification))).ToList();
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        return $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}