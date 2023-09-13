using Moq;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Team.Application;
using SherpaBackEnd.Team.Domain;
using SherpaBackEnd.Team.Infrastructure.Http.Dto;

namespace SherpaBackEnd.Tests.Services;

public class TeamMemberServiceTest
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamMemberService _teamMemberService;

    public TeamMemberServiceTest()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _teamMemberService = new TeamMemberService(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ShouldCallAddTeamMemberToTeamAsyncFromTeamRepository()
    {
        var teamId = Guid.NewGuid();

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var addTeamMemberDto = new AddTeamMemberDto(teamId, teamMember);

        await _teamMemberService.AddTeamMemberToTeamAsync(addTeamMemberDto);

        _mockTeamRepository.Verify(_ => _.AddTeamMemberToTeamAsync(addTeamMemberDto.TeamId, addTeamMemberDto.TeamMember), Times.Once);
    }

    [Fact]
    public async Task ShouldCallGetAllTeamMembersAsyncFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");

        await _teamMemberService.GetAllTeamMembersAsync(teamId);

        _mockTeamRepository.Verify(_ => _.GetAllTeamMembersAsync(teamId), Times.Once);
    }

    [Fact]
    public async Task ShouldGetAllTeamMembersAsAListFromTeamRepository()
    {
        const string teamName = "New team";
        var teamId = Guid.NewGuid();

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "New Member", "Developer", "JohnDoe@google.com");
        var teamMembers = new List<TeamMember>() { teamMember };

        _mockTeamRepository.Setup(_ => _.GetAllTeamMembersAsync(teamId)).ReturnsAsync(teamMembers);

        var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(teamId);
        Assert.IsType<List<TeamMember>>(allTeamMembers);
        Assert.Contains(teamMember, teamMembers);
        Assert.True(teamMembers.Count() == 1);
    }

    [Fact]
    public async Task ShouldGetAnEmptyListOfTeamMembersFromTeamRepository()
    {
        var emptyTeamMembersList = new List<TeamMember>();
        _mockTeamRepository.Setup(_ => _.GetAllTeamMembersAsync(It.IsAny<Guid>())).ReturnsAsync(emptyTeamMembersList);

        var allTeamMembers = await _teamMemberService.GetAllTeamMembersAsync(It.IsAny<Guid>());
        Assert.IsType<List<TeamMember>>(allTeamMembers);
        Assert.True(!allTeamMembers.Any());
    }
}