using System.Security.Claims;
using MongoDB.Bson;

namespace Elira.Api.Services;

public interface ICurrentUserService
{
    ObjectId? UserId { get; }
}

public class CurrentUserService : ICurrentUserService
{
    public ObjectId? UserId { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated ?? false)
        {
           var  userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           UserId = ObjectId.Parse(userId);
        }
    }
}