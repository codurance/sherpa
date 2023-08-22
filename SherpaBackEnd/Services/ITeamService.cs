using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamService
{
    Task<Team> DeprecatedAddTeamAsync(Team team);
    Task<Team?> DeprecatedGetTeamByIdAsync(Guid guid);
    Task<Team?> GetTeamByIdAsync(Guid guid);
    Task<Team?> UpdateTeamByIdAsync(Team team);

    Task AddTeamAsync(Team newTeam);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}