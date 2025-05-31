using MongoDB.Bson.Serialization.Attributes;

namespace Elira.Api.Domain.User;

public class User : BaseEntity
{
    [BsonElement("username")]
    public string Username { get; set; } = null!;

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = null!;


}