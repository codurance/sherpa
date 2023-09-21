using MongoDB.Bson.Serialization.Attributes;

namespace SherpaBackEnd.User.Domain;

public class User
{
    // TODO: Move this to MUser when implemented
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id;
    public string Name;

    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}