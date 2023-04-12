namespace SherpaBackEnd.Dtos;

public class GroupDTO
{
    public Guid id { get; set; }
    public String name { get; set; }

    public GroupDTO(string name)
    {
        this.id = Guid.NewGuid();
        this.name = name;
    }
}