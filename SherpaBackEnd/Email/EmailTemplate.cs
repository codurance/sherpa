namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string Recipient { get; }
    public string Url { get; }

    public EmailTemplate(string recipient, string url)
    {
        Recipient = recipient;
        Url = url;
    }
}