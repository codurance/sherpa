using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IGroupDataService
{
    public Task<List<Group>?> getGroups();
}