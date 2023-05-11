using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace SherpaBackEnd.Services.Email;

public class SesEmailService : IEmailService
{

    private readonly AmazonSimpleEmailServiceClient _amazonSimpleEmailService;

    public SesEmailService()
    {
        _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(
            RegionEndpoint.EUCentral1);
    }
    
    public SesEmailService(string accessKey, string secretKey)
    {
        _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(
            new BasicAWSCredentials(accessKey, secretKey),
            RegionEndpoint.EUCentral1);
    }
    

    public async Task<string> SendEmail(string subject, List<string> recipients)
    {
        recipients = new List<string>
        {
            "elena.sokolova@codurance.com",
            "javier.raez@codurance.com"
        };
        var messageId = "";
        try
        {
            var response = await _amazonSimpleEmailService.SendEmailAsync(
                new SendEmailRequest
                {
                    Destination = new Destination
                    {
                        ToAddresses = recipients
                    },
                    Message = new Message
                    {
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = "Hello Student"
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = "Hello Student"
                            }
                        },
                        Subject = new Content
                        {
                            Charset = "UTF-8",
                            Data = subject
                        }
                    },
                    Source = "ismael.sendros@codurance.com"
                });
            messageId = response.MessageId;
        }
        catch (Exception ex)
        {
            Console.WriteLine("SendEmailAsync failed with exception: " + ex.Message);
        }
        return messageId;
    }
}