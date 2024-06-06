using System.Net;

namespace SherpaBackEnd.Email.Application;

public interface IEmailSender
{
    Task<HttpStatusCode> SendEmailsWith<T>(T emailsTemplate);
}