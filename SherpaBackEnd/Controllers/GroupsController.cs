using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController
{

    private IGroupRepository _groupRepository;

    public GroupsController(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    [HttpGet()]
    
    public async Task<ActionResult<IEnumerable<Group>>> GetGroupsAsync()
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

    [HttpPost()]
    public async Task<ActionResult<Group>> AddGroup(Group group)
    {
        if (group.Name is null || group.Name == string.Empty)
        {
            return new BadRequestResult();
        }
        await Task.Run(() =>_groupRepository.AddGroup(group));
        return new OkResult();
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<Group>> GetGroupAsync(Guid guid)
    {
        var group = await _groupRepository.GetGroup(guid);

        if (group is null)
        {
            return new NotFoundResult();
        }
            
        return new OkObjectResult(group);
    }
}