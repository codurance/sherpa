using System.Net;

namespace SherpaBackEnd.Email.Application;

public class EmailService : IEmailService
{
    public EmailService(IEmailTemplateAdapter emailTemplateAdapter, IEmailSender emailSender)
    {
        throw new NotImplementedException();
    }

    public Task<HttpStatusCode> SendEmailsWith(List<EmailTemplate> emailTemplates)
    {
        throw new NotImplementedException();
    }
}