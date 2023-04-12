using SherpaBackEnd.Dtos;
using SherpaBackEnd.Helpers;

namespace SherpaBackEnd.Model;

public interface IGroupRepository
{

    Task<IEnumerable<GroupDTO>> getGroups();

    void AddGroup(GroupDTO group);
}