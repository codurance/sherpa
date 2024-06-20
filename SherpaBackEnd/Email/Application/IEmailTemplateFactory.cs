using SherpaBackEnd.Email.Templates;

namespace SherpaBackEnd.Email.Application;

public interface IEmailTemplateFactory
{
    [Obsolete]
    EmailTemplate CreateEmailTemplate(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications);
    EmailTemplate CreateEmailTemplate(CreateEmailTemplateDto createEmailTemplateDto);
}