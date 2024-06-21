using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string TemplateName { get; }

    public List<Recipient> Recipients { get; }

    public EmailTemplate(string templateName, List<Recipient> recipients)
    {
        TemplateName = templateName;
        Recipients = recipients;
    }
}