using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class InMemoryTeamMemberService : ITeamMemberService
{
    public List<TeamMember> GetMembers()
    {
        return new List<TeamMember>
        {
            new(Guid.NewGuid(), "Smith", "CEO", "email1@mail.com"),
            new(Guid.NewGuid(), "Hardy", "CP", "email2@mail.com")
        };
    }
}