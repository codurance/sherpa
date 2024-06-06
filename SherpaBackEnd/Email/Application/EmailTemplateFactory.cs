namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string _baseAnswerSurveyUrl = "sherpa.com/answer-survey/";

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
        return _baseAnswerSurveyUrl + notification.Id;
    }
}