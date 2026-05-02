namespace AuthenticationService.Controllers.Dtos;

// Minimal DTO til login
public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}