using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email.Templates.NewSurvey;

public class NewSurveyEmailTemplate: EmailTemplate
{
    public NewSurveyEmailTemplate(string templateName, string surveyTitle, DateTime? surveyDeadline, List<Recipient> recipients) : base(templateName, surveyTitle, surveyDeadline, recipients)
    {
    }
}