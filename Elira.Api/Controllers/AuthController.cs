using Elira.Api.Domain.User;
using Elira.Api.Dto;
using Elira.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService, JwtService jwtService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<Result<string>> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterUserAsync(request.Username, request.Password);
        if (!result)  return Result<string>.Failure("User Already Exists");
        return Result<string>.Success("User Registered");
    }

    [HttpPost("login")]
    public async Task<Result<string>> Login([FromBody] LoginRequest request)
    {
        var data = await authService.ValidateUserAsync(request.Username, request.Password);
        if (data.vendor == null || data.user == null) return Result<string>.Failure("Invalid username or password");

        var token = jwtService.GenerateToken(data.user.Id.ToString());
        return  Result<string>.Success(token);
    }
}