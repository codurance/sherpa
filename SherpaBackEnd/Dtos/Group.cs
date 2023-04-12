using System.Collections;

namespace SherpaBackEnd.Dtos;

public class Group
{
    public Guid Id { get; }
    public IEnumerable<GroupMember> Members { get; set; } = new List<GroupMember>();

    public Group()
    {
        Id = Guid.NewGuid();
    }
}