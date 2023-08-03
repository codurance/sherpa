using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamService
{
    Task<IEnumerable<Team>> DeprecatedGetAllTeamsAsync();

    Task<Team> DeprecatedAddTeamAsync(Team team);
    Task<Team?> GetTeamByIdAsync(Guid guid);
    Task<Team?> UpdateTeamByIdAsync(Team team);

    Task AddTeamAsync(Team newTeam);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}