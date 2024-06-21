namespace SherpaBackEnd.Email.Application;

public class Recipient
{
    public string Name { get; }
    public string Email { get; }
    public string Url { get; }
    public string HtmlBody { get; set; }
    public string TextBody { get; set; }

    public Recipient(string name, string email, string url)
    {
        Name = name;
        Email = email;
        Url = url;
    }

    protected bool Equals(Recipient other)
    {
        return Name == other.Name && Email == other.Email && Url == other.Url;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Recipient)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Email, Url);
    }
}