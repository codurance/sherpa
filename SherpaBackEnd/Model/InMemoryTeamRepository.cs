using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Model;

public class InMemoryTeamRepository : ITeamRepository
{
    private readonly List<Team> _teams;
    private readonly Dictionary<Guid, Team> _dataSet;

    public InMemoryTeamRepository()
    {
        _dataSet = new Dictionary<Guid, Team>();
        
        var anotherTeamWithMembers = new Team("Team B")
        {
            Members = new List<TeamMember>
            {
                new (Guid.NewGuid(), "Ross", "Painter", "bob@gmail.com"),
            }
        };


        var teamWithMembers = new Team("Team A")
        {
            Members = new List<TeamMember>
            {
                new (Guid.NewGuid(), "Anne", "QA", "mary@gmail.com"),
                new (Guid.NewGuid(), "Smith", "CEO", "bobby@gmail.com"),
                new (Guid.NewGuid(), "Hardy", "CP", "bobber@gmail.com")
            }
        };

        _dataSet.Add(teamWithMembers.Id, teamWithMembers);
        _dataSet.Add(anotherTeamWithMembers.Id, anotherTeamWithMembers);
    }

    public InMemoryTeamRepository(List<Team> teams)
    {
        _teams = teams;
    }

    public async Task AddTeamAsync(Team newTeam)
    {
        _teams.Add(newTeam);
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await Task.FromResult(_teams);
    }

    public Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember)
    {
        throw new NotImplementedException();
    }

    public async Task<Team?> GetTeamByIdAsync(Guid teamId)
    {
        return _teams.Find(team => team.Id == teamId);
    }
}