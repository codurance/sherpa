using SherpaBackEnd.Team.Domain;
using ZstdSharp.Unsafe;

namespace SherpaBackEnd.Tests.Builders;

public class TeamBuilder
{
    private Guid _id = Guid.NewGuid();
    private List<TeamMember> _teamMembers = new List<TeamMember>();
    private string _name = "Team";


    public static TeamBuilder ATeam()
    {
        return new TeamBuilder();
    }

    public TeamBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public TeamBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TeamBuilder WithTeamMembers(List<TeamMember> teamMembers)
    {
        _teamMembers = teamMembers;
        return this;
    }
    
    public Team.Domain.Team Build()
    {
        return new Team.Domain.Team
        {
            Id = _id,
            Members = _teamMembers,
            Name = _name,
        };
    }
}