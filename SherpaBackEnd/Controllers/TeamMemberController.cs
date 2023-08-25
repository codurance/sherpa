using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("")]
public class TeamMemberController
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly ILogger<TeamMemberController> _logger;

    public TeamMemberController(ITeamMemberService teamMemberService, ILogger<TeamMemberController> logger)
    {
        _teamMemberService = teamMemberService;
        _logger = logger;
    }

    [HttpPatch("team/{teamId:guid}/members")]
    public async Task<ActionResult> AddTeamMemberToTeamAsync(AddTeamMemberDto addTeamMemberDto)
    {
        try
        {
            await _teamMemberService.AddTeamMemberToTeamAsync(addTeamMemberDto);
            return new CreatedResult("", null);
        }
        catch (Exception error)
        {
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [HttpGet("team/{teamId:guid}/members")]
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