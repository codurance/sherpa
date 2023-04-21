namespace SherpaFrontEnd.Model;

public class GroupMember
{
    
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Position { get; set; }
    
    public string? Email { get; set; }

    public bool IsEditable { get; set; } = false;

    public GroupMember(string? name, string? lastName, string? position, string? email)
    {
        Name = name;
        LastName = lastName;
        Position = position;
        Email = email;
    }

    public GroupMember()
    {
        
    }
}