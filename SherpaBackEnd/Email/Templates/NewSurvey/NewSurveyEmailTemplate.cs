using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email.Templates.NewSurvey;

public class NewSurveyEmailTemplate: EmailTemplate
{
    private static readonly string _templateName = "NewSurvey";

    public NewSurveyEmailTemplate( string surveyTitle, DateTime? surveyDeadline, List<Recipient> recipients) : base(_templateName, surveyTitle, surveyDeadline, recipients)
    {
    }
}