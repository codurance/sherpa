namespace SherpaBackEnd.Email.Templates.NewSurvey;

public interface IEmailTemplateModel
{
    string CreateHtmlBody();
    string CreateTextBody();
}