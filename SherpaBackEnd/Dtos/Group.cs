using System.Collections;

namespace SherpaBackEnd.Dtos;

public class Group
{
    public Guid Id { get; set; }
    public IEnumerable<GroupMember> Members { get; set; } = new List<GroupMember>();
    public string Name { get; set; }

    public Group(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
}