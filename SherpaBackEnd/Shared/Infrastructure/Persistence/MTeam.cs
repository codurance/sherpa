using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MTeam
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public List<string> Members { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; private set; }

    public MTeam(Guid id, List<string> members, string name, bool isDeleted)
    {
        Id = id;
        Members = members;
        Name = name;
        IsDeleted = isDeleted;
    }

    public static MTeam FromTeam(Team.Domain.Team team)
    {
        return new MTeam(team.Id, team.Members.Select(_ => _.Id.ToString()).ToList(), team.Name, team.IsDeleted);
    }
    
    public static Team.Domain.Team ToTeam(MTeam team, IEnumerable<TeamMember> teamMembers)
    {
        return new Team.Domain.Team(team.Id, team.Name, teamMembers.ToList(), team.IsDeleted);
    }
}