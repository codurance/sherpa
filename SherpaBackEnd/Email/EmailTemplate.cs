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

    protected bool Equals(EmailTemplate other)
    {
        return Recipient == other.Recipient && Url == other.Url;
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
        return HashCode.Combine(Recipient, Url);
    }
}