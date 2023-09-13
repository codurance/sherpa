using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Team.Infrastructure.Http;

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
            _logger.LogError(default, error, error.Message);
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [HttpGet("team/{teamId:guid}/members")]
    public async Task<ActionResult<IEnumerable<Domain.Team>>> GetAllTeamMembersAsync(Guid teamId)
    {
        try
        {
            var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(teamId);
            return new OkObjectResult(allTeamMembers);
        }
        catch (Exception error)
        {
            _logger.LogError(default, error, error.Message);
            return error switch
            {
                NotFoundException => new ObjectResult(error)
                    { StatusCode = StatusCodes.Status404NotFound },
                _ => new ObjectResult(error) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}