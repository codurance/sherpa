using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public class InMemoryGroupRepository : IGroupRepository
{
    private readonly List<Group> _emptyGroupList;
    private readonly Dictionary<Guid, Group> _dataSet;

    public InMemoryGroupRepository()
    {
        _dataSet = new Dictionary<Guid, Group>();
        
        var anotherGroupWithMembers = new Group("Group B")
        {
            Members = new List<GroupMember>
            {
                new ("Bob", "Ross", "Painter", "bob@gmail.com"),
            }
        };


        var groupWithMembers = new Group("Group A")
        {
            Members = new List<GroupMember>
            {
                new ("Mary", "Anne", "QA", "mary@gmail.com"),
                new ("Bobby", "Smith", "CEO", "bobby@gmail.com"),
                new ("Bobber", "Hardy", "CP", "bobber@gmail.com")
            }
        };

        _dataSet.Add(groupWithMembers.Id, groupWithMembers);
        _dataSet.Add(anotherGroupWithMembers.Id, anotherGroupWithMembers);
    }

    public InMemoryGroupRepository(List<Group> emptyGroupList)
    {
        _emptyGroupList = emptyGroupList;
    }
    
    public async Task<IEnumerable<Group>> GetGroups()
    {
        return await Task.FromResult(_dataSet.Values.ToList());
    }

    public async Task<Group?> GetGroup(Guid guid)
    {
        return await Task.FromResult(_dataSet.GetValueOrDefault(guid));
    }

    public async Task<Group> AddGroup(Group group)
    {
        _dataSet.Add(group.Id, group);
        return await Task.FromResult(group);
    }

    public async Task<Group> UpdateGroup(Group group)
    {
        _dataSet[group.Id] = group;
        return await Task.FromResult(group);
    }

    public void AddTeam(Group newTeam)
    {
        _emptyGroupList.Add(newTeam);
    }
}