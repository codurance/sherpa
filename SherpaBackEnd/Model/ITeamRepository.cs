using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface ITeamRepository
{
    Task<Team?> DeprecatedGetTeamByIdAsync(Guid guid);
    Task<Team?> GetTeamByIdAsync(Guid guid);

    Task<Team> UpdateTeamByIdAsync(Team team);
    Task<Team> DeprecatedAddTeamAsync(Team team);

    Task AddTeamAsync(Team team);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}