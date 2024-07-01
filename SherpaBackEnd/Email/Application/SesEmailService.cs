using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Newtonsoft.Json;
using SherpaBackEnd.Email.Templates.NewSurvey;

namespace SherpaBackEnd.Email.Application;

public class SesEmailService : IEmailService
{
    private readonly AmazonSimpleEmailServiceClient _amazonEmailService;
    private readonly string _defaultEmail;

    public SesEmailService(AmazonSimpleEmailServiceClient amazonEmailService, string defaultEmail)
    {
        _amazonEmailService = amazonEmailService;
        _defaultEmail = defaultEmail;
    }

    public async Task SendEmailsWith(EmailTemplate emailTemplate)
    {
        try
        {
            await GetTemplate(emailTemplate);
            var request = CreateBulkTemplatedEmailRequest(emailTemplate);
            await _amazonEmailService.SendBulkTemplatedEmailAsync(request);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async Task GetTemplate(EmailTemplate emailTemplate)
    {
        try
        {
            var templateRequest = new GetTemplateRequest()
            {
                TemplateName = emailTemplate.TemplateName
            };
            await _amazonEmailService.GetTemplateAsync(templateRequest);
        }
        catch (Exception e)
        {
            await CreateDefaultSurveyTemplate(emailTemplate);
        }
    }

    private async Task CreateDefaultSurveyTemplate(EmailTemplate emailTemplate)
    {
        try
        {
            var templateRequest = CreateTemplateRequest(emailTemplate);
            await _amazonEmailService.CreateTemplateAsync(templateRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private CreateTemplateRequest CreateTemplateRequest(EmailTemplate emailTemplate)
    {
        return new CreateTemplateRequest
        {
            Template = new Amazon.SimpleEmail.Model.Template
            {
                TemplateName = emailTemplate.TemplateName,
                SubjectPart = emailTemplate.Subject,
                HtmlPart = @"{{html-body}}",
                TextPart = @"{{text-body}}"
            }
        };
    }

    private SendBulkTemplatedEmailRequest CreateBulkTemplatedEmailRequest(EmailTemplate emailTemplate)
    {
        var bulkEmailDestinations = emailTemplate.Recipients.ConvertAll(recipient => new BulkEmailDestination
        {
            Destination = new Destination
            {
                ToAddresses = new List<string>() { recipient.Email }
            },
            ReplacementTemplateData = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {
                    "html-body", recipient.HtmlBody
                },
                {
                    "text-body", recipient.TextBody
                }
            })
        });

        var sendBulkTemplatedEmailRequest = new SendBulkTemplatedEmailRequest
        {
            Destinations = bulkEmailDestinations,
            Source = _defaultEmail,
            Template = emailTemplate.TemplateName,
            DefaultTemplateData = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                { "html-body", "Something went wrong" }, { "text-body", "Something went wrong" }
            }),
            ReturnPath = _defaultEmail
        };
        return sendBulkTemplatedEmailRequest;
    }
}