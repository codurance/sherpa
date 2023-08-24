using SherpaBackEnd.Dtos;
using SherpaBackEnd.Exceptions;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Tests.Repositories;

public class InMemoryTeamRepositoryTest
{
    [Fact]
    public async Task ShouldBeAbleToAddNewTeam()
    {
        var initialList = new List<Team>();
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        const string teamName = "Test Name";
        var newTeam = new Team(teamName);

        await inMemoryTeamRepository.AddTeamAsync(newTeam);

        Assert.Contains(newTeam, initialList);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTeams()
    {
        const string teamName1 = "Team 1";
        var existingTeam1 = new Team(teamName1);
        const string teamName2 = "Team 2";
        var existingTeam2 = new Team(teamName2);

        var initialList = new List<Team>() { existingTeam1, existingTeam2 };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var retrievedTeams = await inMemoryTeamRepository.GetAllTeamsAsync();

        Assert.Equal(new List<Team>() { existingTeam1, existingTeam2 }, retrievedTeams);
    }

    [Fact]
    public async Task ShouldBeAbleToGetTeamById()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Team 1";
        var expectedTeam = new Team(teamId, teamName);

        var initialList = new List<Team>() { expectedTeam };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var actualTeam = await inMemoryTeamRepository.GetTeamByIdAsync(teamId);

        Assert.Equal(expectedTeam, actualTeam);
    }

    [Fact]
    public async Task ShouldBeAbleToAddTeamMemberToTeamWhenThereAreNoTeamMembers()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Team 1";
        var initialTeam = new Team(teamId, teamName);

        var memberId = Guid.NewGuid();
        var teamMember = new TeamMember(memberId, "Name", "Position", "email@gov.com");

        var initialList = new List<Team>() { initialTeam };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        await inMemoryTeamRepository.AddTeamMemberToTeamAsync(teamId, teamMember);

        Assert.Contains(teamMember, initialList[0].Members);
        Assert.True(initialList[0].Members.Count() == 1);
    }

    [Fact]
    public async Task ShouldBeAbleToAddTeamMemberToTeamWhenThereIsAtLeastOneTeamMembers()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Team 1";

        var member1Id = Guid.NewGuid();
        var teamMember1 = new TeamMember(member1Id, "Name1", "Position1", "email1@gov.com");
        var member2Id = Guid.NewGuid();
        var teamMember2 = new TeamMember(member2Id, "Name2", "Position2", "email2@gov.com");
        var teamMembers = new List<TeamMember>() { teamMember1, teamMember2 };

        var member3Id = Guid.NewGuid();
        var teamMember3 = new TeamMember(member3Id, "Name3", "Position3", "email3@gov.com");

        var initialTeam = new Team(teamId, teamName, teamMembers);
        var initialList = new List<Team>() { initialTeam };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        await inMemoryTeamRepository.AddTeamMemberToTeamAsync(teamId, teamMember3);

        Assert.Contains(teamMember3, initialList[0].Members);
        Assert.True(initialList[0].Members.Count() == 3);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTeamMembersFromASpecificTeamId()
    {
        var teamId = Guid.NewGuid();
        const string teamName = "Team 1";

        var member1Id = Guid.NewGuid();
        var teamMember1 = new TeamMember(member1Id, "Name1", "Position1", "email1@gov.com");
        var member2Id = Guid.NewGuid();
        var teamMember2 = new TeamMember(member2Id, "Name2", "Position2", "email2@gov.com");
        var member3Id = Guid.NewGuid();
        var teamMember3 = new TeamMember(member3Id, "Name3", "Position3", "email3@gov.com");

        var teamMembers = new List<TeamMember>() { teamMember1, teamMember2, teamMember3 };

        var initialTeam = new Team(teamId, teamName, teamMembers);
        var initialList = new List<Team>() { initialTeam };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var allTeamMembers = await inMemoryTeamRepository.GetAllTeamMembersAsync(teamId);

        Assert.Equal(teamMembers, allTeamMembers);
        Assert.IsType<List<TeamMember>>(allTeamMembers);
        Assert.True(allTeamMembers.Count() == 3);
    }

    [Fact]
    public async Task ShouldThrowAnExceptionWhenGetTeamByIdAsyncCanNotFindThatTeamId()
    {
        var teamId = Guid.NewGuid();

        var initialList = new List<Team>() { };
        var inMemoryTeamRepository = new InMemoryTeamRepository(initialList);

        var exceptionThrown =
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await inMemoryTeamRepository.GetTeamByIdAsync(teamId));
        Assert.IsType<NotFoundException>(exceptionThrown);
    }
}