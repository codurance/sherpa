using MongoDB.Bson.Serialization.Attributes;

namespace SherpaBackEnd.Model;

public class User
{
    // TODO: Move this to MUser when implemented
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id;
    public string Name;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}