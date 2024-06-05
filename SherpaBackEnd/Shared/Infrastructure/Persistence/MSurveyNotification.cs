using MongoDB.Bson.Serialization.Attributes;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurveyNotification
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    public Guid Survey { get; }

    public Guid TeamMember { get; }
    
    
}