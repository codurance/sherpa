using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface ITeamService
{
    Task<Team?> GetTeamByIdAsync(Guid guid);
    Task AddTeamAsync(Team newTeam);
    Task<IEnumerable<Team>> GetAllTeamsAsync();
}