using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Controllers;

namespace SherpaBackEnd.Tests.Controllers;

public class GroupMembersControllerTests
{

    [Fact]
    public void NotFoundIsReturn()
    {
        var controller = new GroupMembersController();
        
        var actionResult = controller.GetGroupMemberDtos(new Guid());
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
    
    
}