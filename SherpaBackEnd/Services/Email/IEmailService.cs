using System.Net;

namespace SherpaBackEnd.Services.Email;

public interface IEmailService
{
    Task<HttpStatusCode> SendEmail(string subject, List<string> recipients);
}