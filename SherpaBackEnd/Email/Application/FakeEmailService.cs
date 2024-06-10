using System.Net;

namespace SherpaBackEnd.Email.Application;

public class FakeEmailService : IEmailService
{
    public Task<HttpStatusCode> SendEmailsWith(List<EmailTemplate> emailTemplates)
    {
        return new Task<HttpStatusCode>(() => HttpStatusCode.OK);
    }
}