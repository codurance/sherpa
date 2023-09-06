using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SherpaBackEnd.Dtos;
using SherpaBackEnd.Model;

namespace SherpaBackEnd.Repositories.Mongo;

public class MongoTeamRepository : ITeamRepository
{
    private readonly IMongoCollection<MTeam> _teamsCollection;
    private readonly IMongoCollection<MTeamMember> _teamMembersCollection;

    public MongoTeamRepository(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _teamsCollection = mongoDatabase.GetCollection<MTeam>(
            databaseSettings.Value.TeamsCollectionName);

        _teamMembersCollection = mongoDatabase.GetCollection<MTeamMember>(
            databaseSettings.Value.TeamMembersCollectionName);
    }

    public async Task<Dtos.Team?> GetTeamByIdAsync(Guid guid)
    {
        var filter = Builders<MTeam>.Filter.Eq("_id", guid.ToString());
        var mTeam = await _teamsCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        
        if (mTeam == null)
        {
            return null;
        }

        var teamMembers = await PopulateTeamMembers(mTeam.Members);

        return MTeam.ToTeam(mTeam, teamMembers);
    }

    public async Task AddTeamAsync(Dtos.Team team)
    {
        await _teamsCollection.InsertOneAsync(MTeam.FromTeam(team));
    }

    public async Task<IEnumerable<Dtos.Team>> GetAllTeamsAsync()
    {
        var mTeamToTeamMembers = new Dictionary<MTeam, List<TeamMember>>();
        var mTeams = await _teamsCollection.FindAsync(team => true).Result.ToListAsync();
        if (mTeams == null)
        {
            return new List<Dtos.Team>();
        }

        foreach (var mTeam in mTeams)
        {
            var teamMembers = await PopulateTeamMembers(mTeam.Members);
            mTeamToTeamMembers.Add(mTeam, teamMembers);
        }

        var teams = mTeamToTeamMembers.Select(pair => MTeam.ToTeam(pair.Key, pair.Value));

        return teams;
    }

    public async Task AddTeamMemberToTeamAsync(Guid teamId, TeamMember teamMember)
    {
        await _teamMembersCollection.InsertOneAsync(MTeamMember.FromTeamMember(teamMember));

        await _teamsCollection.UpdateOneAsync(
            team => team.Id == teamId,
            Builders<MTeam>.Update.Push("Members", teamMember.Id.ToString()));
    }

    public async Task<IEnumerable<TeamMember>?> GetAllTeamMembersAsync(Guid teamId)
    {
        var team = await GetTeamByIdAsync(teamId);
        return team?.Members;
    }

    private async Task<List<TeamMember>> PopulateTeamMembers(IEnumerable<string> teamMembersIds)
    {
        var filter = Builders<MTeamMember>.Filter.In("_id", teamMembersIds);
        var mTeamMembers = await _teamMembersCollection.FindAsync(filter).Result.ToListAsync();
        return mTeamMembers.Select(MTeamMember.ToTeamMember).ToList();
    }
}