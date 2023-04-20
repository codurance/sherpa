using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class InMemoryGroupMemberService : IGroupMemberService
{
    public List<GroupMember> GetMembers()
    {
        return new List<GroupMember>
        {
            new("Bob", "Smith", "CEO", "email1@mail.com"),
            new("Tom", "Hardy", "CP", "email2@mail.com")
        };
    }
}