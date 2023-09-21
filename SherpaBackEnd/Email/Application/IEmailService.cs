using System.Net;

namespace SherpaBackEnd.Email.Application;

public interface IEmailService
{
    Task<HttpStatusCode> SendEmail(string subject, List<string> recipients);
}