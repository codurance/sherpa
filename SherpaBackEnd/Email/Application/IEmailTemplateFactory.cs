namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    EmailTemplate CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications);
}