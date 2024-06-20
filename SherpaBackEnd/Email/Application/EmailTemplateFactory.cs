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
        // TODO refactor to an actual factory
        var survey = surveyNotifications.First().Survey;
        var title = survey.Title;
        var deadline = survey.Deadline;
        var recipients = surveyNotifications.Select(notification =>
            new Recipient( notification.TeamMember.FullName, notification.TeamMember.Email, CreateAnswerSurveyUrl(notification))).ToList();
        return new EmailTemplate("NewSurvey", title, deadline, recipients);
    }

    private string CreateAnswerSurveyUrl(SurveyNotification.Domain.SurveyNotification notification)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        return $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/answer-survey/{notification.Id}";
    }
}