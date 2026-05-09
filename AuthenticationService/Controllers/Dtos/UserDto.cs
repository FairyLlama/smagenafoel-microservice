using AuthenticationService.Enums;

namespace AuthenticationService.Controllers.Dtos;

// DTO for bruger data
public class UserDto
{
    public int Id { get; private set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Language { get; set; }
    public required int Age { get; set; }
    public required string Phone { get; set; }
    public UserRoles Role { get; private set; }
}