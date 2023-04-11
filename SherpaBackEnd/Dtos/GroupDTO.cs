namespace SherpaBackEnd.Dtos;

public class GroupDTO
{
    public String name { get; set; }

    public GroupDTO(string name)
    {
        this.name = name;
    }
}