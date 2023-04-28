namespace SherpaBackEnd.Services.Email;

public interface IEmailService
{
    Task<string> sendEmail(string subject, List<string> recipients);
}