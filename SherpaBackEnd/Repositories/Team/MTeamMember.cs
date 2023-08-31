using MongoDB.Bson.Serialization.Attributes;
using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Repositories;

internal class MTeamMember
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }

    public MTeamMember(Guid id, string fullName, string position, string email)
    {
        Id = id;
        FullName = fullName;
        Position = position;
        Email = email;
    }
    
    public static TeamMember ToTeamMember(MTeamMember mTeamMember)
    {
        return new TeamMember(mTeamMember.Id, mTeamMember.FullName, mTeamMember.Position, mTeamMember.Email);
    }
    
    public static MTeamMember FromTeamMember(TeamMember teamMember)
    {
        return new MTeamMember(teamMember.Id, teamMember.FullName, teamMember.Position, teamMember.Email);
    }
}