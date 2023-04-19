using SherpaBackEnd.Dtos;
using SherpaBackEnd.Helpers;

namespace SherpaBackEnd.Model;

public interface IGroupRepository
{

    Task<IEnumerable<Group>> GetGroups();

    Task<Group?> GetGroup(Guid guid);

    Task<Group?> UpdateGroup(Group group);
    Task<Group> AddGroup(Group group);
    void DeleteGroup(Guid guid);
}