using SherpaBackEnd.Dtos;
using SherpaBackEnd.Helpers;

namespace SherpaBackEnd.Model;

public class InMemoryGroupRepository : IGroupRepository
{
    private Dictionary<Guid, Group> _dataSet;

    public InMemoryGroupRepository()
    {
        _dataSet = new Dictionary<Guid, Group>();
        
        var groupWithMembers = new Group("Group A");
        var groupWithoutMembers = new Group("Group B");
        groupWithMembers.Members = new List<GroupMember>
        {
            new (),
            new (),
            new ()
        };

        _dataSet.Add(groupWithMembers.Id, groupWithMembers);
        _dataSet.Add(groupWithoutMembers.Id, groupWithoutMembers);
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        return await Task.FromResult(_dataSet.Values.ToList());
    }

    public async Task<Group> GetGroup(Guid guid)
    {
        return await Task.FromResult(_dataSet.GetValueOrDefault(guid));
    }

    public void AddGroup(Group group)
    {
        throw new NotImplementedException();
    }
    
    
}