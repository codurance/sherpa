using BlazorApp.Model;

namespace BlazorApp.Services;

public interface IGroupMemberService
{
    
    List<GroupMember> GetMembers();
}