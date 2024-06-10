using System.Net;

namespace SherpaBackEnd.Email.Application;

public interface IEmailService
{
    Task<HttpStatusCode> SendEmailsWith(EmailTemplate emailTemplate);
}