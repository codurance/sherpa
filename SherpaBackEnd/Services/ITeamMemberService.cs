using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamMemberService
{
    public Task AddTeamMemberToTeamAsync(AddTeamMemberDto addTeamMemberDto);
    public Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId);
}