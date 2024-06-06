namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    List<EmailTemplate> CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications);
}