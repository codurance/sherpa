namespace SherpaBackEnd.Dtos;

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
}