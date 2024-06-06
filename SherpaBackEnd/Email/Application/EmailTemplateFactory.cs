namespace SherpaBackEnd.Email.Application;

public class EmailTemplateFactory : IEmailTemplateFactory
{
    private string _baseAnswerSurveyUrl = "sherpa.com/answer-survey/";

    public List<EmailTemplate> CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications)
    {
        return surveyNotifications.Select(notification =>
            new EmailTemplate(notification.TeamMember.Email, _baseAnswerSurveyUrl + notification.Id)).ToList();
    }
}