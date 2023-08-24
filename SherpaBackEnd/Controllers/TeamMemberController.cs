using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class TeamMemberController
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly ILogger<TeamMemberController> _logger;

    public TeamMemberController(ITeamMemberService teamMemberService, ILogger<TeamMemberController> logger)
    {
        _teamMemberService = teamMemberService;
        _logger = logger;
    }

    public async Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember)
    {
        await _teamMemberService.AddTeamMemberToTeamAsync(teamId, teamMember);
    }

    public async Task<ActionResult<IEnumerable<Team>>> GetAllTeamMembersAsync(Guid teamId)
    {
        try
        {
            var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(teamId);
            if (allTeamMembers == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(allTeamMembers);

        }
        catch (Exception error)
        {
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}