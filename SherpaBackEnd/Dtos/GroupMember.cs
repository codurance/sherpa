namespace SherpaBackEnd.Dtos;

public class GroupMember
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }

    public GroupMember(string name, string lastName, string position)
    {
        Name = name;
        LastName = lastName;
        Position = position;
    }
}