using System.Net;
using SherpaBackEnd.SurveyNotification.Domain;

namespace SherpaBackEnd.Email.Application;

public interface IEmailServicePoC
{
    Task<HttpStatusCode> SendEmail(string templateName, List<string> recipients);
    Task<HttpStatusCode> SendEmailWith(List<EmailTemplate> templateRequest);
}