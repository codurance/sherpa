using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Newtonsoft.Json;
using SherpaBackEnd.Exceptions;

namespace SherpaBackEnd.Email.Application;

public class SesEmailService : IEmailService
{
    private const string TemplateName = "answer_survey_template";
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
            await GetTemplate(TemplateName);
            var request = CreateBulkTemplatedEmailRequest(emailTemplate, TemplateName);
            await _amazonEmailService.SendBulkTemplatedEmailAsync(request);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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
            var templateRequest = CreateTemplateRequest(TemplateName);
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
                        {{html-body}}
                    </p>
                    <a target=""_blank"" href=""{{personal-link}}"">Answer questionnaire</a>
                ",
                TextPart = @"
                    {{text-body}}
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
            ReplacementTemplateData = JsonConvert.SerializeObject(new Dictionary<string, string>{{"html-body", string.Join("<br />", emailTemplate.Body.Split("\n"))},{"text-body", emailTemplate.Body}, {"personal-link", recipient.Url}})
        });
        
        var sendBulkTemplatedEmailRequest = new SendBulkTemplatedEmailRequest
        {
            Destinations = bulkEmailDestinations,
            Source = _defaultEmail,
            Template = templateName,
            DefaultTemplateData = JsonConvert.SerializeObject(new Dictionary<string, string>{{"html-body", "Something went wrong"}, {"text-body", "Something went wrong"}, {"personal-link", "Missing link"}}),
            ReturnPath = _defaultEmail
        };
        return sendBulkTemplatedEmailRequest;
    }
}