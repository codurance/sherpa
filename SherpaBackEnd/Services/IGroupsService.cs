using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Services;

public interface IGroupsService
{
    Task<IEnumerable<Group>> GetGroups();

    Task<Group> AddGroup(Group group);
    Task<Group?> GetGroup(Guid guid);
    Task<Group?> UpdateGroup(Group group);
}