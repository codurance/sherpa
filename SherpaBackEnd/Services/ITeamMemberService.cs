using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamMemberService
{
    public Task AddTeamMemberToTeam(Guid id, TeamMember member);
}