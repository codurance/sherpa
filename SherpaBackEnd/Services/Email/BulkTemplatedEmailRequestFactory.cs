using Amazon.SimpleEmail.Model;

namespace SherpaBackEnd.Services.Email;

public class BulkTemplatedEmailRequestFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BulkTemplatedEmailRequestFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    private static SendBulkTemplatedEmailRequest CreateBulkTemplatedEmailRequest()
    {
        List<string> recipients;
        recipients = new List<string>
        {
            "elena.sokolova@codurance.com",
            "javier.raez@codurance.com"
        };
        var bulkEmailDestinations = recipients.ConvertAll(email => new BulkEmailDestination
        {
            Destination = new Destination
            {
                ToAddresses = new List<string>{email}
            },
            ReplacementTemplateData = $"{{\"personal-link\":{Getpersonallink(email)}}}"
        });


        var sendBulkTemplatedEmailRequest = new SendBulkTemplatedEmailRequest
        {
            Destinations = bulkEmailDestinations,
            Source = "javier.raez@codurance.com",
            Template = SesEmailService.PendingSurveyTemplate
        };
        return sendBulkTemplatedEmailRequest;
    }
    
    private static string Getpersonallink(string input)
    {
        throw new NotImplementedException();
    }
}