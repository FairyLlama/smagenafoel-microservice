using AuthenticationService.Enums;

namespace AuthenticationService.Models;

// Bruger entitet
public class User
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Language { get; set; }
    public required int Age { get; set; }
    public UserRole Role { get; private set; }
}