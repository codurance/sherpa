namespace SherpaBackEnd.Email.Application;

public class Recipient
{
    public string Email { get; }
    public string Url { get; }

    public Recipient(string email, string url)
    {
        Email = email;
        Url = url;
    }

    protected bool Equals(Recipient other)
    {
        return Email == other.Email && Url == other.Url;
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
        return HashCode.Combine(Email, Url);
    }
}