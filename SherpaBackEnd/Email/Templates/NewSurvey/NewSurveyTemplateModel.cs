namespace SherpaBackEnd.Email.Templates.NewSurvey;

public class NewSurveyTemplateModel : IEmailTemplateModel
{
    public string Name { get; set; }
    public string SurveyName { get; set; }
    public string? Deadline { get; set; }
    public string Url { get; set; }

    public string CreateHtmlBody()
    {
        return new NewSurveyHtmlTemplate()
        {
            TemplateModel = this
        }.TransformText();
    }

    public string CreateTextBody()
    {
        return new NewSurveyTextTemplate()
        {
            TemplateModel = this
        }.TransformText();
    }
}