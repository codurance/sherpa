namespace SherpaBackEnd.Dtos;

public class Team
{
    public Guid Id { get; set; }
    public IEnumerable<TeamMember> Members { get; set; } = new List<TeamMember>();
    public string Name { get; set; }
    public bool IsDeleted { get; private set; } = false;


    public Team(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
    
}