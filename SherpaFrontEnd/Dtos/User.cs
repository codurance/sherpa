namespace SherpaFrontEnd.Dtos;

public class User
{
    public Guid Id;
    public string Name;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}