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
    public ActionResult<IEnumerable<GroupDTO>> getGroups()
    {
        List<GroupDTO> groups = _groupRepository.getGroups();
        if (groups.Count == 0)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(groups);
    }

}