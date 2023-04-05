using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface IGroupMemberService
{
    
    List<GroupMember> GetMembers();
}