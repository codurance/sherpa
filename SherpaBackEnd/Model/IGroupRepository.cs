using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public interface IGroupRepository
{
    List<GroupDTO> getGroups();
}