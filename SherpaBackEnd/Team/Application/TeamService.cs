using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Team.Application;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<Domain.Team?> GetTeamByIdAsync(Guid guid)
    {
        return await _teamRepository.GetTeamByIdAsync(guid);
    }

    public async Task AddTeamAsync(Domain.Team newTeam)
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

    public async Task<IEnumerable<Domain.Team>> GetAllTeamsAsync()
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