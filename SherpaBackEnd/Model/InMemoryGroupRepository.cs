using SherpaBackEnd.Dtos;
using SherpaBackEnd.Helpers;

namespace SherpaBackEnd.Model;

public class InMemoryGroupRepository : IGroupRepository
{

    private DataContext _dataContext;

    public InMemoryGroupRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<GroupDTO>> getGroups()
    {
        return _dataContext.Groups.AsEnumerable().ToArray();
    }

    public void AddGroup(GroupDTO group)
    {
        _dataContext.Groups.Add(group);
        _dataContext.SaveChanges();
    }
}