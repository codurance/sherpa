using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamService
{
    Task<Team?> GetTeamByIdAsync(Guid guid);
    Task<Team?> UpdateTeamByIdAsync(Team team);

    Task AddTeamAsync(Team newTeam);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}