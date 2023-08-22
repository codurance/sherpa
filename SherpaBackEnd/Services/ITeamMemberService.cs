using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamMemberService
{
    public Task addTeamMemberToTeam(Guid id, TeamMember member);
}