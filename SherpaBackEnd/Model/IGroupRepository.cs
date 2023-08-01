using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IGroupRepository
{

    Task<IEnumerable<Group>> GetGroups();

    Task<Group?> GetGroup(Guid guid);

    Task<Group> UpdateGroup(Group group);
    Task<Group> AddGroup(Group group);

    void AddTeam(Group group);
}