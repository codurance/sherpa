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
        await _inMemoryTeamRepository.AddTeamMemberToTeamAsync(teamId, teamMember);
    }

    public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync(Guid teamId)
    {
        return await _inMemoryTeamRepository.GetAllTeamMembersAsync(teamId);
    }
}