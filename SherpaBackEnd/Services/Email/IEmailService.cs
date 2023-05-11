namespace SherpaBackEnd.Services.Email;

public interface IEmailService
{
    Task<string> SendEmail(string subject, List<string> recipients);
}