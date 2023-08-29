using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamRepository _inMemoryTeamRepository;

    public TeamMemberService(ITeamRepository inMemoryTeamRepository)
    {
        _inMemoryTeamRepository = inMemoryTeamRepository;
    }

    public async Task AddTeamMemberToTeamAsync(AddTeamMemberDto addTeamMemberDto)
    {
        await _inMemoryTeamRepository.AddTeamMemberToTeamAsync(addTeamMemberDto.TeamId, addTeamMemberDto.TeamMember);
    }

    public async Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId)
    {
        var allTeamMembersAsync = await _inMemoryTeamRepository.GetAllTeamMembersAsync(teamId);

        if (allTeamMembersAsync == null)
        {
            throw new NotFoundException("Team not found");
        }

        return allTeamMembersAsync;
    }
}