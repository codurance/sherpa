namespace SherpaFrontEnd.Model;

public class Group
{
    public String Name { get; set; }
    
    public Guid Id { get; }

    public Group(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}