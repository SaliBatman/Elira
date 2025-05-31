using Elira.Api.Domain.Scan;
using Elira.Api.Domain.User;
using Elira.Api.Dto;
using MongoDB.Driver;

namespace Elira.Api.Services;

public class ScanService
{
    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<Vendor> _vendors;
    private readonly IMongoCollection<ScanRequests> _scanRequests;
    private readonly ICurrentUserService _currentUserService;

    public ScanService(IConfiguration config, ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDb:Database"]);
        _users = database.GetCollection<User>("Users");
        _vendors = database.GetCollection<Vendor>("Vendors");
        _scanRequests = database.GetCollection<ScanRequests>("ScanRequests");
    }

    public async Task<ScanRequests> AddNewScan(ScanFormDto form)
    {
        var userQuery = await _users.FindAsync(s=> s.Id == _currentUserService.UserId);
        var user = userQuery.FirstOrDefault();
        var vendorQuery = await _vendors.FindAsync(s=>s.Moderators.Any(d=> d == _currentUserService.UserId));
        var vendor = vendorQuery.FirstOrDefault();
        var model = new ScanRequests()
        {
            CustomerName = form.CustomerName,
            Title = form.Title,
            PhoneNumber = form.PhoneNumber,
            VisitReason = form.VisitReason,
            UserId = user.Id,   
            VendorId = vendor.Id
        };
        _scanRequests.InsertOneAsync(model);
        return model;
    }
}