using SherpaBackEnd.Dtos;
using SherpaBackEnd.Helpers;

namespace SherpaBackEnd.Model;

public class InMemoryGroupRepository : IGroupRepository
{
    private Dictionary<Guid, Group> _dataSet;

    public InMemoryGroupRepository()
    {
        _dataSet = new Dictionary<Guid, Group>();
        
        var anotherGroupWithMembers = new Group("Group B");
        anotherGroupWithMembers.Members = new List<GroupMember>
        {
            new ("Bob", "Ross", "Painter"),
        };
        
        
        var groupWithMembers = new Group("Group A");
        groupWithMembers.Members = new List<GroupMember>
        {
            new ("Mary", "Anne", "QA"),
            new ("Bobby", "Smith", "CEO"),
            new ("Bobber", "Hardy", "CP")
        };

        _dataSet.Add(groupWithMembers.Id, groupWithMembers);
        _dataSet.Add(anotherGroupWithMembers.Id, anotherGroupWithMembers);
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        return await Task.FromResult(_dataSet.Values.ToList());
    }

    public async Task<Group> GetGroup(Guid guid)
    {
        return await Task.FromResult(_dataSet.GetValueOrDefault(guid));
    }

    public async void AddGroup(Group group)
    {
        _dataSet.Add(Guid.NewGuid(), group);
    }

    public void DeleteGroup(Guid isAny)
    {
        throw new NotImplementedException();
    }
}