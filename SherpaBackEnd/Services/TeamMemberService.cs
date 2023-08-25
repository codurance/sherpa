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

    public async Task AddTeamMemberToTeamAsync(AddTeamMemberDto addTeamMemberDto)
    {
        try
        {
            await _inMemoryTeamRepository.AddTeamMemberToTeamAsync(addTeamMemberDto.TeamId, addTeamMemberDto.TeamMember);
        }
        catch (Exception error)
        {
            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);
        }
    }

    public async Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId)
    {
        try
        {
            return await _inMemoryTeamRepository.GetAllTeamMembersAsync(teamId);
        }
        catch (Exception error){

            throw new ConnectionToRepositoryUnsuccessfulException(error.Message, error);

        }
    }
}