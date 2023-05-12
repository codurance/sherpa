using System.Net;
using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace SherpaBackEnd.Services.Email;

public class SesEmailService : IEmailService
{
    public const string PendingSurveyTemplate = "pending_survey_template";
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


    public async Task<HttpStatusCode> SendEmail(string subject, List<string> recipients)
    {
        var httpStatusCode = HttpStatusCode.BadRequest;
        
        try
        {
            await GetTemplate(PendingSurveyTemplate);
        
            var sendBulkTemplatedEmailRequest = CreateBulkTemplatedEmailRequest();
        
            var response = await _amazonSimpleEmailService.SendBulkTemplatedEmailAsync(
                sendBulkTemplatedEmailRequest);
            httpStatusCode = response.HttpStatusCode;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("SendEmailAsync failed with exception: " + ex.Message);
        }

        return httpStatusCode;
    }

    private SendBulkTemplatedEmailRequest CreateBulkTemplatedEmailRequest()
    {
        List<string> recipients = new()
        {
            "elena.sokolova@codurance.com",
            "javier.raez@codurance.com"
        };
        var bulkEmailDestinations = recipients.ConvertAll(email => new BulkEmailDestination
        {
            Destination = new Destination
            {
                ToAddresses = new List<string> { email }
            },
            ReplacementTemplateData = $"{{\"personal-link\":\"{GetPersonalLink(email)}\"}}"
        });


        var sendBulkTemplatedEmailRequest = new SendBulkTemplatedEmailRequest
        {
            Destinations = bulkEmailDestinations,
            Source = "javier.raez@codurance.com",
            Template = PendingSurveyTemplate,
            DefaultTemplateData = "{\"personal-link\":\"default@email.com\"}"
        };
        return sendBulkTemplatedEmailRequest;
    }

    private string GetPersonalLink(string email)
    {
        return email;
    }

    private async Task CreatePendingSurveyTemplate()
    {
        try
        {
            await _amazonSimpleEmailService.CreateTemplateAsync(new CreateTemplateRequest
            {
                Template = new Template
                {
                    TemplateName = PendingSurveyTemplate,
                    SubjectPart = "Hey you have pending survey",
                    HtmlPart = @"
<p>
    In order to access the survey click the following link:
</p>
<a href=""{{personal-link}}"">{{personal-link}}</a>
",
                    TextPart = @"
In order to access the survey click the following link:
{{personal-link}}
"
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task GetTemplate(string template)
    {
        try
        {
            await _amazonSimpleEmailService.GetTemplateAsync(new GetTemplateRequest
            {
                TemplateName = template
            });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await CreatePendingSurveyTemplate();
        }
    }

}