namespace BlazorApp.Model;

public class GroupMember
{
    public String Name { get; set; }
    public String LastName { get; set; }
    public String Position { get; set; }

    public GroupMember(string name, string lastName, string position)
    {
        Name = name;
        LastName = lastName;
        Position = position;
    }
}