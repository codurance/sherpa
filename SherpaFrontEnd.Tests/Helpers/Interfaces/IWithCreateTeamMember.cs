using SherpaFrontEnd.Dtos.Team;

namespace BlazorApp.Tests.Helpers.Interfaces;

public interface IWithCreateTeamMember
{
    Task CreateTeamMember(AddTeamMemberDto addTeamMemberDto);
}