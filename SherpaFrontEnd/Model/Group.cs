namespace SherpaFrontEnd.Model;

public class Group
{
    public String Name { get; set; }
    
    public Guid Id { get; set; }
    
    public List<GroupMember>? Members { get; set; }
    
// public Group(string name, Guid guid)
//     {
//         Id = guid;
//         Name = name;
//     }
}