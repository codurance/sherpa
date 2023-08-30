using SherpaFrontEnd.Dtos.Team;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface ITeamMemberService
{
    List<TeamMember> GetMembers();
}