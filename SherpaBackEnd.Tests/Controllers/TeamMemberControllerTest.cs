using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Http;
using SherpaBackEnd.Team.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Controllers;

public class TeamMemberControllerTest
{
    private readonly Mock<ITeamMemberService> _mockTeamMemberService;
    private readonly TeamMemberController _teamMemberController;

    public TeamMemberControllerTest()
    {
        _mockTeamMemberService = new Mock<ITeamMemberService>();
        var logger = Mock.Of<ILogger<TeamMemberController>?>();
        _teamMemberController = new TeamMemberController(_mockTeamMemberService.Object, logger);
    }

    [Fact]
    public async Task ShouldCallAddTeamMemberToTeamAsyncFromService()
    {
        var teamId = Guid.NewGuid();

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var addTeamMemberDto = new AddTeamMemberDto(teamId, teamMember);
        
        await _teamMemberController.AddTeamMemberToTeamAsync(addTeamMemberDto);

        _mockTeamMemberService.Verify(_ => _.AddTeamMemberToTeamAsync(addTeamMemberDto), Times.Once);
    }
    
    [Fact]
    public async Task ShouldRetrieveOkWhenNoProblemsFoundWhileAddingMember()
    {
        var teamId = Guid.NewGuid();

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var addTeamMemberDto = new AddTeamMemberDto(teamId, teamMember);
        Assert.IsType<CreatedResult>(await _teamMemberController.AddTeamMemberToTeamAsync(addTeamMemberDto));
    }
    
    [Fact]
    public async Task ShouldReturnErrorIfAMemberCanNotBeAdded()
    {
        var notSuccessfulAdding = new ConnectionToRepositoryUnsuccessfulException("Cannot perform add member function.");
        _mockTeamMemberService.Setup(_ => _.AddTeamMemberToTeamAsync(It.IsAny<AddTeamMemberDto>())).ThrowsAsync(notSuccessfulAdding);

        var allTeamMembers = await _teamMemberController.AddTeamMemberToTeamAsync(It.IsAny<AddTeamMemberDto>());
        var resultObject = Assert.IsType<ObjectResult>(allTeamMembers);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }

    [Fact]
    public async Task ShouldCallGetAllTeamMemberByTeamAsyncFromService()
    {
        var teamId = Guid.NewGuid();

        await _teamMemberController.GetAllTeamMembersAsync(teamId);

        _mockTeamMemberService.Verify(_ => _.GetAllTeamMembersAsync(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnOkWhenTeamMembersListRetrievedWhileGettingTeamMembers()
    {
        var teamId = Guid.NewGuid();
        var member1Id = Guid.NewGuid();
        var teamMember1 = new TeamMember(member1Id, "Name1", "Position1", "email1@gov.com");
        var member2Id = Guid.NewGuid();
        var teamMember2 = new TeamMember(member2Id, "Name2", "Position2", "email2@gov.com");

        var teamMemberList = new List<TeamMember>() { teamMember1, teamMember2 };

        _mockTeamMemberService.Setup(service => service.GetAllTeamMembersAsync(teamId))
            .ReturnsAsync(teamMemberList);

        var getAllTeamsAction = await _teamMemberController.GetAllTeamMembersAsync(teamId);
        var okObjectResult = Assert.IsType<OkObjectResult>(getAllTeamsAction.Result);
        var teamMembers = Assert.IsType<List<TeamMember>>(okObjectResult.Value);
        Assert.True(teamMembers.Any());
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenGetTeamByIdAsyncCanNotFindThatTeamId()
    {
        var teamId = Guid.NewGuid();
        _mockTeamMemberService.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ThrowsAsync(new NotFoundException("demo"));

        var allTeamMembers = await _teamMemberController.GetAllTeamMembersAsync(teamId);
        var notFoundResult = Assert.IsType<ObjectResult>(allTeamMembers.Result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnErrorIfATeamCanNotBeFoundByTheTeamId()
    {
        var teamId = Guid.NewGuid();
        var notSuccessfulGetting = new ConnectionToRepositoryUnsuccessfulException("Cannot perform get team function.");
        _mockTeamMemberService.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ThrowsAsync(notSuccessfulGetting);

        var allTeamMembers = await _teamMemberController.GetAllTeamMembersAsync(teamId);
        var resultObject = Assert.IsType<ObjectResult>(allTeamMembers.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }
}