using Amazon.SimpleEmail.Model;

namespace SherpaBackEnd.Email.Application;

public class EmailServiceRequestFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailServiceRequestFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CreateTemplateRequest CreateTemplateRequest(string templateName)
    {
        return new CreateTemplateRequest
        {
            Template = new Amazon.SimpleEmail.Model.Template
            {
                TemplateName = templateName,
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
        };
    }
    public SendBulkTemplatedEmailRequest CreateBulkTemplatedEmailRequest(string templateName)
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
            Template = templateName,
            DefaultTemplateData = "{\"personal-link\":\"default@email.com\"}"
        };
        return sendBulkTemplatedEmailRequest;
    }

    private string GetPersonalLink(string email)
    {
        var httpContextRequest = _httpContextAccessor.HttpContext?.Request;
        var baseUrl = $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}/";
        
        return $"{baseUrl}{email}";
    }

    public GetTemplateRequest GetTemplateRequest(string templateName)
    {
        return new GetTemplateRequest
        {
            TemplateName = templateName
        };
    }
}