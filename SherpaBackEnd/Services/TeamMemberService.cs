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

    public async Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember)
    {
        _inMemoryTeamRepository.AddTeamMemberToTeamAsync(teamId, teamMember);
    }

    public async Task GetAllTeamMembersAsync(Guid teamId)
    {
        throw new NotImplementedException();
    }
}