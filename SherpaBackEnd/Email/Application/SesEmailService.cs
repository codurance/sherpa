using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;

namespace SherpaBackEnd.Email.Application;

public class SesEmailService : IEmailService
{
    private const string PendingSurveyTemplate = "pending_survey_template";
    private readonly AmazonSimpleEmailServiceClient _amazonSimpleEmailService;
    private readonly EmailServiceRequestFactory _emailServiceRequestFactory;

    public SesEmailService(IHttpContextAccessor httpContextAccessor)
    {
        _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(
            RegionEndpoint.EUCentral1);
        _emailServiceRequestFactory = new EmailServiceRequestFactory(httpContextAccessor);
    }

    public SesEmailService(IHttpContextAccessor httpContextAccessor, string accessKey, string secretKey)
    {
        _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(
            new BasicAWSCredentials(accessKey, secretKey),
            RegionEndpoint.EUCentral1);
        _emailServiceRequestFactory = new EmailServiceRequestFactory(httpContextAccessor);
    }
    
    public async Task<HttpStatusCode> SendEmail(string subject, List<string> recipients)
    {
        var httpStatusCode = HttpStatusCode.BadRequest;
        
        try
        {
            await GetTemplate(PendingSurveyTemplate);
        
            var sendBulkTemplatedEmailRequest = _emailServiceRequestFactory
                                                .CreateBulkTemplatedEmailRequest(PendingSurveyTemplate);
        
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


    private async Task CreatePendingSurveyTemplate()
    {
        try
        {
            var templateRequest = _emailServiceRequestFactory.CreateTemplateRequest(PendingSurveyTemplate);
            await _amazonSimpleEmailService.CreateTemplateAsync(templateRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task GetTemplate(string templateName)
    {
        try
        {
            var templateRequest = _emailServiceRequestFactory.GetTemplateRequest(templateName);
            await _amazonSimpleEmailService.GetTemplateAsync(templateRequest);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await CreatePendingSurveyTemplate();
        }
    }
}