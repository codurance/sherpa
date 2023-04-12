using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
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
    
    public async Task<ActionResult<IEnumerable<GroupDTO>>> getGroups()
    {
        IEnumerable<GroupDTO> groups = await _groupRepository.getGroups();
        if (!groups.Any())
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(groups);
    }

    [HttpPost("/groups")]
    public ActionResult<GroupDTO> addGroup(GroupDTO group)
    {
        _groupRepository.AddGroup(group);
        return new OkResult();
    }

}