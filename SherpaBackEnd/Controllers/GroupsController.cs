using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Controllers;

[ApiController]
public class GroupsController
{

    private IGroupRepository _groupRepository;

    public GroupsController(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    [HttpGet("/groups")]
    
    public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups()
    {
        IEnumerable<GroupDTO> groups = new List<GroupDTO>();
        try
        { 
            groups = await _groupRepository.getGroups();
        }
        catch(RepositoryException repositoryException)
        {
            var error = new { message = "Internal server error. Try again later" };
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        if (!groups.Any())
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(groups);
    }

    [HttpPost("/groups")]
    public ActionResult<GroupDTO> AddGroup(GroupDTO group)
    {
        _groupRepository.AddGroup(group);
        return new OkResult();
    }

}