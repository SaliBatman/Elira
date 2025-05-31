using MongoDB.Bson;

namespace Elira.Api.Domain.Scan;

public class ScanRequests : BaseEntity
{
    public ObjectId VendorId { get; set; }
    public ObjectId UserId { get; set; }
    public string Title { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string VisitReason { get; set; }
    public List<ImageUrl> ImageUrls { get; set; }
}