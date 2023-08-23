using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamRepository _inMemoryTeamRepository;

    public TeamMemberService(ITeamRepository inMemoryTeamRepository)
    {
        _inMemoryTeamRepository = inMemoryTeamRepository;
    }

    public async Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember)
    {
        try
        {
            await _inMemoryTeamRepository.AddTeamMemberToTeamAsync(teamId, teamMember);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }

    public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync(Guid teamId)
    {
        try
        {
            return await _inMemoryTeamRepository.GetAllTeamMembersAsync(teamId);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }
}