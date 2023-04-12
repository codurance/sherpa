using System.Collections;

namespace SherpaBackEnd.Dtos;

public class Group
{
    public Guid Id { get; }
    public IEnumerable<GroupMember> Members { get; } = new List<GroupMember>();

    public Group(string name)
    {
        Id = Guid.NewGuid();
    }
}