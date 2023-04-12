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

    public async Task<IEnumerable<Group>> GetGroups()
    {
        return _dataContext.Groups.AsEnumerable().ToArray();
    }

    public Task<Group> GetGroup(Guid guid)
    {
        throw new NotImplementedException();
    }

    public void AddGroup(Group group)
    {
        _dataContext.Groups.Add(group);
        _dataContext.SaveChanges();
    }
}