namespace SherpaBackEnd.Team.Application;

public interface ITeamService
{
    Task<Domain.Team?> GetTeamByIdAsync(Guid guid);
    Task AddTeamAsync(Domain.Team newTeam);
    Task<IEnumerable<Domain.Team>> GetAllTeamsAsync();
}