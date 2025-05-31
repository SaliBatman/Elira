using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Elira.Api.Domain;

public class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public DateTime Created { get; set; } =DateTime.Now;
    public bool IsActive { get; set; }
}