using Microsoft.AspNetCore.Mvc;
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
            new ("Bob", "Ross", "Painter", "bob@gmail.com"),
        };
        
        
        var groupWithMembers = new Group("Group A");
        groupWithMembers.Members = new List<GroupMember>
        {
            new ("Mary", "Anne", "QA", "mary@gmail.com"),
            new ("Bobby", "Smith", "CEO", "bobby@gmail.com"),
            new ("Bobber", "Hardy", "CP", "bobber@gmail.com")
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

    public async Task<Group> AddGroup(Group group)
    {
        _dataSet.Add(group.Id, group);
        return await Task.FromResult(group);
    }

    public void DeleteGroup(Guid guid)
    {
        var group = _dataSet.First(g => g.Key == guid);
        _dataSet.Remove(guid);
    }

    public async Task<Group> UpdateGroup(Group group)
    {
        _dataSet[group.Id] = group;
        return await Task.FromResult(group);
    }
}