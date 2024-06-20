namespace SherpaBackEnd.Email.Templates.NewSurvey;

public class NewSurveyEmailTemplateDto : CreateEmailTemplateDto
{
    public List<SurveyNotification.Domain.SurveyNotification> SurveyNotifications { get; set; }

    public NewSurveyEmailTemplateDto(List<SurveyNotification.Domain.SurveyNotification> surveyNotifications): base("NewSurvey")
    {
        SurveyNotifications = surveyNotifications;
    }
}