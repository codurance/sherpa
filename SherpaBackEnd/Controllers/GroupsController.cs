using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController
{
    private readonly IGroupsService _groupsService;


    public GroupsController(IGroupsService groupsService)
    {
        this._groupsService = groupsService;
    }

    [HttpGet()]
    
    public async Task<ActionResult<IEnumerable<Group>>> GetGroupsAsync()
    {
        IEnumerable<Group> groups;
        try
        { 
            groups = await _groupsService.GetGroups();
            
            if (!groups.Any())
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(groups);
        }
        catch(RepositoryException repositoryException)
        {
            var error = new { message = "Internal server error. Try again later" };
            return new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    [HttpPost()]
    public async Task<ActionResult<Group>> AddGroup(Group group)
    {
        if (group.Name is null || group.Name == string.Empty)
        {
            return new BadRequestResult();
        }
        await Task.Run(() => _groupsService.AddGroup(group));
        return new OkResult();
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<Group>> GetGroupAsync(Guid guid)
    {
        var group = await _groupsService.GetGroup(guid);

        if (group is null)
        {
            return new NotFoundResult();
        }
            
        return new OkObjectResult(group);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult<Group>> DeleteGroupAsync(Guid guid)
    {
        var group = await _groupsService.GetGroup(guid);
        if (group is null)
        {
            return new NotFoundResult();
        }
        group.Delete();
        await _groupsService.UpdateGroup(group);
        return new OkResult();
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Group>> UpdateGroup(Guid guid,Group group)
    {
        var groupFound = await _groupsService.GetGroup(guid);
        if (groupFound is null)
        {
            return new NotFoundResult();
        }

        group.Id = guid;
        await _groupsService.UpdateGroup(group);
        return new OkObjectResult(group);
    }
}