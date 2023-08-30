namespace SherpaBackEnd.Dtos;

public class Team
{
    public Guid Id { get; set; }
    public List<TeamMember>? Members { get; set; } = new List<TeamMember>();
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
    
}