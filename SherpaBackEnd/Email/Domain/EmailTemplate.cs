using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string TemplateName { get; }

    public List<Recipient> Recipients { get; }

    public DateTime? SurveyDeadline { get; }

    public EmailTemplate(string templateName, DateTime? surveyDeadline, List<Recipient> recipients)
    {
        TemplateName = templateName;
        SurveyDeadline = surveyDeadline;
        Recipients = recipients;
    }
}