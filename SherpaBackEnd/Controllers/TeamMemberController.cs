using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

public class TeamMemberController
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly ILogger<TeamController> _logger;

    public TeamMemberController(ITeamMemberService teamMemberService, ILogger<TeamController> logger)
    {
        _teamMemberService = teamMemberService;
        _logger = logger;
    }

    public async Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember TeamMember)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<IEnumerable<Team>>> GetAllTeamMembersAsync()
    {
        throw new NotImplementedException();
    }
}