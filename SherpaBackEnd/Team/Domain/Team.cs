namespace SherpaBackEnd.Team.Domain;

public class Team
{
    public Guid Id { get; set; }
    
    public List<TeamMember> Members { get; set; } = new List<TeamMember>();
    public string Name { get; set; }
    public bool IsDeleted { get; private set; } = false;


    public Team(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Team(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Team()
    {
    }
    
    public Team(Guid id, string name, List<TeamMember>? members, bool isDeleted)
    {
        Members = members;
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
    }

    public Team(Guid id, string name, List<TeamMember>? members)
    {
        Members = members;
        Id = id;
        Name = name;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    public bool IsMemberOfTeam(Guid teamMemberId)
    {
        return this.Members.Exists(member => member.Id.Equals(teamMemberId));
    }
}