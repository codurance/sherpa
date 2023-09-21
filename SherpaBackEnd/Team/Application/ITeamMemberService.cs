using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Team.Application;

public interface ITeamMemberService
{
    public Task AddTeamMemberToTeamAsync(AddTeamMemberDto addTeamMemberDto);
    public Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId);
}