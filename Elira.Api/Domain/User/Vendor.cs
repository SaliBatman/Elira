using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Elira.Api.Domain.User;

public class Vendor : BaseEntity
{
    [BsonElement("name")]

    public string Name { get; set; }
    [BsonElement("phone")]

    public string Phone { get; set; }
    [BsonElement("address")]

    public string Address { get; set; }
    [BsonElement("moderators")]

    public ObjectId[] Moderators { get; set; }
}