namespace SherpaBackEnd.Email;

public class EmailTemplateRequest
{
    public string Recipient { get; }
    public string Url { get; }

    public EmailTemplateRequest(string recipient, string url)
    {
        Recipient = recipient;
        Url = url;
    }
}