using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IGroupDataService
{
    public Task<List<Group>?> GetGroups();
    Task<Group?> GetGroup(Guid guid);
    Task DeleteGroup(Guid guid);
    Task PutGroup(Group group);
    Task AddGroup(Group group);
}