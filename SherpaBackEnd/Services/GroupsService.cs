using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Services;

public class GroupsService : IGroupsService
{
    private readonly IGroupRepository _groupRepository;

    public GroupsService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        var groups = await _groupRepository.GetGroups();
        return groups.Where(g => !g.IsDeleted).ToList();
    }

    public async Task<Group> AddGroup(Group group)
    {
        return await _groupRepository.AddGroup(group);
    }

    public async Task<Group?> GetGroup(Guid guid)
    {
        return await _groupRepository.GetGroup(guid);
    }

    public async Task<Group?> UpdateGroup(Group group)
    {
        return await _groupRepository.UpdateGroup(group);
    }
}