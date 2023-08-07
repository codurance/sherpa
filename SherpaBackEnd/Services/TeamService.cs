using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IEnumerable<Team>> DeprecatedGetAllTeamsAsync()
    {
        var teams = await _teamRepository.DeprecatedGetAllTeams();
        return teams.Where(g => !g.IsDeleted).ToList();
    }

    public async Task<Team> DeprecatedAddTeamAsync(Team team)
    {
        return await _teamRepository.DeprecatedAddTeamAsync(team);
    }

    public async Task<Team?> DeprecatedGetTeamByIdAsync(Guid guid)
    {
        return await _teamRepository.DeprecatedGetTeamByIdAsync(guid);
    }
    
    public async Task<Team?> GetTeamByIdAsync(Guid guid)
    {
        return await _teamRepository.GetTeamByIdAsync(guid);
    }

    public async Task<Team?> UpdateTeamByIdAsync(Team team)
    {
        return await _teamRepository.UpdateTeamByIdAsync(team);
    }

    public async Task AddTeamAsync(Team newTeam)
    {
        try
        {
            await _teamRepository.AddTeamAsync(newTeam);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        try
        {
            return await _teamRepository.GetAllTeamsAsync();
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }
}