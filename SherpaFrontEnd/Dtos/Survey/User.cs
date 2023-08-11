namespace SherpaFrontEnd.Dtos.Survey;

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

// This file is placed inside Survey temporarily 