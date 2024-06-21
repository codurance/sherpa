using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email.Templates.NewSurvey;

public class NewSurveyEmailTemplate: EmailTemplate
{
    private static readonly string _templateName = "NewSurvey";

    public NewSurveyEmailTemplate(List<Recipient> recipients) : base(_templateName, recipients)
    {
    }
}