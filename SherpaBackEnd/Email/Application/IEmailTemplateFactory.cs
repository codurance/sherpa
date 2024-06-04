namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    void CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications);
}