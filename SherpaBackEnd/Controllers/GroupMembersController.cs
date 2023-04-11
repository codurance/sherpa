using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Controllers;

[ApiController]
public class GroupMembersController : ControllerBase
{
    // GET
    [HttpGet("group/{groupId:guid}/group-members")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<GroupMemberDto>> GetGroupMemberDtos(Guid groupId)
    {
        var memberDtos = Enumerable.Empty<GroupMemberDto>();
        var groupMemberDtos = new ActionResult<IEnumerable<GroupMemberDto>>(memberDtos);
        return NotFound();
    }
}