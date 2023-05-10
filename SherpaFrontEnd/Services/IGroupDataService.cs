using System.Net;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IGroupDataService
{
    public Task<List<Group>?> GetGroups();
    Task<Group?> GetGroup(Guid guid);
    Task<HttpStatusCode> DeleteGroup(Guid guid);
    Task PutGroup(Group group);
    Task AddGroup(Group group);
}