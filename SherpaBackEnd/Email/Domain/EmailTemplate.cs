using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string TemplateName { get; }

    public List<Recipient> Recipients { get; }

    public string SurveyTitle { get; set; }

    public DateTime? SurveyDeadline { get; }

    public EmailTemplate(string templateName, string surveyTitle, DateTime? surveyDeadline, List<Recipient> recipients)
    {
        TemplateName = templateName;
        SurveyTitle = surveyTitle;
        SurveyDeadline = surveyDeadline;
        Recipients = recipients;
    }
}