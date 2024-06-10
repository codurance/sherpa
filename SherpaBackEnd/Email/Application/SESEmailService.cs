using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace SherpaBackEnd.Email.Application;

public class SESEmailService : IEmailService
{
    private readonly AmazonSimpleEmailServiceClient _amazonEmailService;

    public SESEmailService(AmazonSimpleEmailServiceClient amazonEmailService)
    {
        _amazonEmailService = amazonEmailService;
    }

    public async Task<HttpStatusCode> SendEmailsWith(EmailTemplate emailTemplate)
    {
        try
        {
            List<string> emailAddresses = emailTemplate.Recipients.Select(recipient => recipient.Email).ToList();
            var request = new SendEmailRequest()
            {
                Destination = new Destination()
                {
                    ToAddresses = emailAddresses,
                },
                Message = new Message()
                {
                    Body = new Body()
                    {
                        Text = new Content
                        {
                            Charset = "UTF-8",
                            Data = EmailBody(emailTemplate)
                        }
                    },
                    Subject = new Content
                    {
                        Charset = "UTF-8",
                        Data = "Answer this survey"
                    }
                },
                Source = "paula.masutier@codurance.com"
                
                
            };
            var response = await _amazonEmailService.SendEmailAsync(request);
            return response.HttpStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string EmailBody(EmailTemplate emailTemplate)
    {
        string emailBody = string.Join(emailTemplate.Body, "\n\n", emailTemplate.Recipients.Select(recipient =>
            $"Please visit our website at the following link:\n{recipient.Url}"));
        return emailBody;
    }
}