using MongoDB.Bson;

namespace Elira.Api.Domain.Scan;

public class ImageUrl
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public string Path { get; set; }
}