namespace SherpaFrontEnd.Model;

public class Group
{
    public String Name { get; set; }
    
    public Guid Id { get; }
    
    public List<GroupMember>? Members { get; set; }
    
public Group(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}