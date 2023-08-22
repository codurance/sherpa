using System.Net;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController
{
    private readonly ITeamService _teamService;
    private readonly ILogger _logger;
    
    public TeamController(ITeamService teamService, ILogger<TeamController> logger)
    {
        _teamService = teamService;
        _logger = logger;
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<Team>> GetTeamByIdAsync(Guid guid)
    {
        var team = await _teamService.GetTeamByIdAsync(guid);

        if (team is null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(team);
    }

    [HttpPost]
    public async Task<ActionResult> AddTeamAsync(Team newTeam)
    {
        try
        {
            await _teamService.AddTeamAsync(newTeam);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetAllTeamsAsync()
    {
        try
        {
            var allTeamsAsync = await _teamService.GetAllTeamsAsync();
            return new OkObjectResult(allTeamsAsync);

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