using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamRepository _inMemoryTeamRepository;

    public TeamMemberService(ITeamRepository inMemoryTeamRepository)
    {
        _inMemoryTeamRepository = inMemoryTeamRepository;
    }

    public Task AddTeamMemberToTeam(Guid id, TeamMember member)
    {
        throw new NotImplementedException();
    }
}