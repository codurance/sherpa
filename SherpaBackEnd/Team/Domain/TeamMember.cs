namespace SherpaBackEnd.Team.Domain;

public class TeamMember
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }

    public string Email { get; set; }

    public TeamMember(Guid id, string fullName, string position, string email)
    {
        Id = id;
        Position = position;
        Email = email;
        FullName = fullName;
    }

    protected bool Equals(TeamMember other)
    {
        return Id.Equals(other.Id) && FullName == other.FullName && Position == other.Position && Email == other.Email;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TeamMember)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FullName, Position, Email);
    }

    public static bool operator ==(TeamMember? left, TeamMember? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TeamMember? left, TeamMember? right)
    {
        return !Equals(left, right);
    }
}