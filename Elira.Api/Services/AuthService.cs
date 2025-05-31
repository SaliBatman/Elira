using Elira.Api.Domain.User;
using MongoDB.Driver;
using BCrypt.Net;
namespace Elira.Api.Services;

public class AuthService
{
    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<Vendor> _vendors;

    public AuthService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDb:Database"]);
        _users = database.GetCollection<User>("Users");
        _vendors = database.GetCollection<Vendor>("Vendors");
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task<bool> RegisterUserAsync(string username, string password)
    {
        var existing = await GetByUsernameAsync(username);
        if (existing != null) return false;

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        await _users.InsertOneAsync(user);
        return true;
    }

    public async Task<(Vendor? vendor,User? user)> ValidateUserAsync(string username, string password)
    {
        var user = await GetByUsernameAsync(username);
        Console.WriteLine(user.PasswordHash);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return (null, null);

        var vendor = await _vendors.FindAsync(s => s.Moderators.Any(d => d == user.Id));
        if (vendor == null) return (null, null);    
        
        return (await vendor.FirstOrDefaultAsync(), user);
    }
}