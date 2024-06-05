namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    List<EmailTemplateRequest> CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications);
}