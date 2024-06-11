using System.Net;

namespace SherpaBackEnd.Email.Application;

public interface IEmailService
{
    Task SendEmailsWith(EmailTemplate emailTemplate);
}