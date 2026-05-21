namespace BlazorLogin.Models;

// Model til håndtering af brugere
public class UserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Language { get; set; }
    public int Age { get; set; }
    public required string Phone { get; set; }
    public required string Role { get; set; }

}