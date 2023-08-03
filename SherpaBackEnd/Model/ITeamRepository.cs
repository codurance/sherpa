using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface ITeamRepository
{

    Task<IEnumerable<Team>> DeprecatedGetAllTeams();

    Task<Team?> GetTeamByIdAsync(Guid guid);

    Task<Team> UpdateTeamByIdAsync(Team team);
    Task<Team> DeprecatedAddTeamAsync(Team team);

    Task AddTeamAsync(Team team);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}