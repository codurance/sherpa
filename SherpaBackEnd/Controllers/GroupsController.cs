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
    
    public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
    {
        IEnumerable<Group> groups = new List<Group>();
        try
        { 
            groups = await _groupRepository.GetGroups();
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
    public ActionResult<Group> AddGroup(Group group)
    {
        _groupRepository.AddGroup(group);
        return new OkResult();
    }

    public async Task<ActionResult<Group>> GetGroup(Guid guid)
    {
        var group = await _groupRepository.GetGroup(guid);

        if (group is null)
        {
            return new NotFoundResult();
        }
            
        return new OkObjectResult(group);
    }
}