using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class InMemoryTeamMemberService : ITeamMemberService
{
    public List<TeamMember> GetMembers()
    {
        return new List<TeamMember>
        {
            new("Bob", "Smith", "CEO", "email1@mail.com"),
            new("Tom", "Hardy", "CP", "email2@mail.com")
        };
    }
}