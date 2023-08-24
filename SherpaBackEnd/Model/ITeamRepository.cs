using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface ITeamRepository
{
    Task<Team?> GetTeamByIdAsync(Guid guid);
    Task AddTeamAsync(Team team);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
    Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember);
    Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId);
}