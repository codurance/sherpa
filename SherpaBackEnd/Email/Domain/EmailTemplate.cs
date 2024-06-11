using SherpaBackEnd.Email.Application;

namespace SherpaBackEnd.Email;

public class EmailTemplate
{
    public string Body { get; }
    public List<Recipient> Recipients { get; }
    public EmailTemplate(string body, List<Recipient> recipients)
    {
        Body = body;
        Recipients = recipients;
    }

    protected bool Equals(EmailTemplate other)
    {
        return Body == other.Body && Recipients.Equals(other.Recipients);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EmailTemplate)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Body, Recipients);
    }
}