using AuthenticationService.Controllers.Dtos;

namespace AuthenticationService.Interfaces;

// Authentication interface til service
public interface IAuth
{
    Task Register(UserDto user);
    Task<string> Login(LoginDto dto);
}