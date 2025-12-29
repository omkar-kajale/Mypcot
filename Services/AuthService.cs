using Microsoft.IdentityModel.Tokens;
using Mypcot.Models.Dto;
using Mypcot.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mypcot.Services;

public interface IAuthService
{
    Task<(bool, string)> Login(LoginDto request);
}

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _config;

    public AuthService(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _config = configuration;
    }
    public async Task<(bool, string)> Login(LoginDto request)
    {
        var login = await _authRepository.GetLogin(request.Login);
        if (login == null)
            return (false, "Login not found");

        if (login.Password != request.Password)
            return (false, "Incorrect password");

        var token = GenerateToken(request.Login);
        return (true, token);
    }

    private string GenerateToken(string login)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: new[] { new Claim(ClaimTypes.Name, login) },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
