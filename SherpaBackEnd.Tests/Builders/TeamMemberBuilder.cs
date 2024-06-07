using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Tests.Builders;

public class TeamMemberBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _position = "Team Member";
    private string _email = "team-member@codurance.com";
    private string _fullName = "John Doe";

    public static TeamMemberBuilder ATeamMember()
    {
        return new TeamMemberBuilder();
    }

    public TeamMemberBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public TeamMemberBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public TeamMemberBuilder WithPosition(string position)
    {
        _position = position;
        return this;
    }

    public TeamMemberBuilder WithFullName(string fullName)
    {
        _fullName = fullName;
        return this;
    }

    public TeamMember Build()
    {
        return new TeamMember(_id, _fullName, _position, _email);
    }
}