using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Controllers.Dtos;
using AuthenticationService.Enums;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Services;

// Authentication service implementering
public class AuthService(ApplicationDbContext dbContext) : IAuth
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    // Registrer bruger med data fra frontend (HTTP)
    public async Task Register(UserDto user)
    {
        var hasher = new PasswordHasher<User>();

        User newUser = new()
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Phone = user.Phone,
            Age = user.Age,
            Language = user.Language
        };
        // Krypter password så det er gemt sikkert i databasen
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();
    }

    // Bruger login med DTO
    public async Task<string> Login(LoginDto dto)
    {
        // Find bruger vha. email hvis de ikke eksisterer retuner med fejl
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return "Invalid email or password";
        }

        // Tjek om det indtastede password stemmer overens med hash fra "register"
        var hasher = new PasswordHasher<User>();
        var verified = hasher.VerifyHashedPassword(user, user.Password, dto.Password);

        // Hvis ikke succes returner med fejl
        if (verified != PasswordVerificationResult.Success)
        {
            return "Invalid password";
        }

        // Lav JWT data ud fra environment variabler
        var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt__Key")!));
        var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer")!;
        var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience")!;
        var credentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);

        // Lav claims ud fra den verificerede bruger
        var claims = new[]
        {
            new Claim("sub", dto.Email),
            new Claim("uid", user.Id.ToString()),
            new Claim(ClaimTypes.Role, Roles.UserRole[user.Role]),
        };

        // Lav ny JWT med al ovenstående data
        var authToken = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credentials
        );

        // returner valid JWT
        return new JwtSecurityTokenHandler().WriteToken(authToken);
    }
}