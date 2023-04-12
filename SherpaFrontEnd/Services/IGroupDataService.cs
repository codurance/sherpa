using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IGroupDataService
{
    public List<Group>? getGroups();
}