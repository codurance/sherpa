using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string TemplateName { get; }
    public string Subject { get; }

    public List<Recipient> Recipients { get; }

    public EmailTemplate(string templateName, string subject, List<Recipient> recipients)
    {
        TemplateName = templateName;
        Subject = subject;
        Recipients = recipients;
    }
}