namespace BlazorApp.Model;

public class GroupMember
{
    private String Name;
    private String LastName;
    private String Position;

    public GroupMember(string name, string lastName, string position)
    {
        Name = name;
        LastName = lastName;
        Position = position;
    }

    public string Name1
    {
        get => Name;
        set => Name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName1
    {
        get => LastName;
        set => LastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Position1
    {
        get => Position;
        set => Position = value ?? throw new ArgumentNullException(nameof(value));
    }
}