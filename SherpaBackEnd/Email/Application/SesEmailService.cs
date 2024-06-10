using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Newtonsoft.Json;
using SherpaBackEnd.Exceptions;

namespace SherpaBackEnd.Email.Application;

public class SesEmailService : IEmailService
{
    private const string DefaultTemplate = "default_template";
    private readonly AmazonSimpleEmailServiceClient _amazonEmailService;

    public SesEmailService(AmazonSimpleEmailServiceClient amazonEmailService)
    {
        _amazonEmailService = amazonEmailService;
    }

    public async Task<HttpStatusCode> SendEmailsWith(EmailTemplate emailTemplate)
    {
        var httpStatusCode = HttpStatusCode.BadRequest;
        try
        {
            /*var deleteTemplateRequest = new DeleteTemplateRequest()
            {
                TemplateName = DefaultTemplate
            };
            await _amazonEmailService.DeleteTemplateAsync(deleteTemplateRequest);*/
            await GetTemplate(DefaultTemplate);
            var request = CreateBulkTemplatedEmailRequest(emailTemplate, DefaultTemplate);
            var response = await _amazonEmailService.SendBulkTemplatedEmailAsync(request);
            
            httpStatusCode = response.HttpStatusCode;
        }
        catch (Exception e)
        {
            return HttpStatusCode.InternalServerError;
        }

        return httpStatusCode;
    }
    
    private async Task GetTemplate(string templateName)
    {
        try
        {
            var templateRequest = new GetTemplateRequest()
            {
                TemplateName = templateName
            };
            await _amazonEmailService.GetTemplateAsync(templateRequest);

        }
        catch (Exception e)
        {
            await CreateDefaultSurveyTemplate();
        }
    }
    
    private async Task CreateDefaultSurveyTemplate()
    {
        try
        {
            var templateRequest = CreateTemplateRequest(DefaultTemplate);
            await _amazonEmailService.CreateTemplateAsync(templateRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public CreateTemplateRequest CreateTemplateRequest(string templateName)
    {
        return new CreateTemplateRequest
        {
            Template = new Amazon.SimpleEmail.Model.Template
            {
                TemplateName = templateName,
                SubjectPart = "Pending Survey",
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
        };
    }
    public SendBulkTemplatedEmailRequest CreateBulkTemplatedEmailRequest(EmailTemplate emailTemplate,
        string templateName)
    {
        var bulkEmailDestinations = emailTemplate.Recipients.ConvertAll(recipient => new BulkEmailDestination
        {
            Destination = new Destination
            {
                ToAddresses = new List<string>(){ recipient.Email}
            },
            ReplacementTemplateData = $"\"personal-link\":\"{recipient.Url}\"}}"
        });
        
        var sendBulkTemplatedEmailRequest = new SendBulkTemplatedEmailRequest
        {
            Destinations = bulkEmailDestinations,
            Source = "paula.masutier@codurance.com",
            Template = templateName,
            DefaultTemplateData = "{ \"personal-link\":\"default@email.com\"}",
            ReturnPath = "paula.masutier@codurance.com"
        };
        return sendBulkTemplatedEmailRequest;
    }
}