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
    
    public async Task<ActionResult<IEnumerable<Team>>> DeprecatedGetAllTeamsAsync()
    {
        IEnumerable<Team> teams;
        try
        {
            teams = await _teamService.DeprecatedGetAllTeamsAsync();

            if (!teams.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(teams);
        }
        catch (ConnectionToRepositoryUnsuccessfulException repositoryException)
        {
            _logger.LogError(default, repositoryException, repositoryException.Message);
            var error = new { message = "Internal server error. Try again later" };
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
    
    public async Task<ActionResult<Team>> DeprecatedAddTeamAsync(Team team)
    {
        Console.WriteLine("backend " + team.Name);
        if (String.IsNullOrEmpty(team.Name))
        {
            return new BadRequestResult();
        }

        await Task.Run(() => _teamService.DeprecatedAddTeamAsync(team));
        return new OkResult();
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

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult<Team>> DeleteTeamByIdAsync(Guid guid)
    {
        var team = await _teamService.DeprecatedGetTeamByIdAsync(guid);
        if (team is null)
        {
            return new NotFoundResult();
        }

        team.Delete();
        await _teamService.UpdateTeamByIdAsync(team);
        return new OkResult();
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Team>> UpdateTeamAsync(Guid guid, Team team)
    {
        // TODO use (Team team) signature to not expose too much
        var teamFound = await _teamService.DeprecatedGetTeamByIdAsync(guid);
        if (teamFound is null)
        {
            return new NotFoundResult();
        }

        team.Id = guid;
        await _teamService.UpdateTeamByIdAsync(team);
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