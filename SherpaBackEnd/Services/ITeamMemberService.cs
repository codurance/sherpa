using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamMemberService
{
    public Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember);
    public Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync(Guid teamId);
}