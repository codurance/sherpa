using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Controllers;

[ApiController]
public class GroupMembersController : ControllerBase
{
    // GET
    [HttpGet("group/{groupId:guid}/group-members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<GroupMember>> GetGroupMemberDtos(Guid groupId)
    {
        var memberDtos = Enumerable.Empty<GroupMember>();
        var groupMemberDtos = new ActionResult<IEnumerable<GroupMember>>(memberDtos);
        return NotFound();
    }
}