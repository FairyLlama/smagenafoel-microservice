using BlazorLogin.Models;
using System.Net;

namespace BlazorLogin.Interfaces;

// Interface til injection
public interface IAuthService
{
    Task<HttpStatusCode> RegisterUserAsync(UserDto dto);
    Task<LoginResponse> LoginUserAsync(LoginDto dto);
}