using AuthenticationService.Controllers.Dtos;
using AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

// Controller til authentication, laver endpoints til håndtering af brugere
[ApiController]
[Route("auth/[controller]")]
public class AuthController(IAuth auth) : ControllerBase
{
    private readonly IAuth _auth = auth;

    // Registrering af bruger
    [HttpPost("/register")]
    public async Task<ActionResult> Register([FromBody] UserDto user)
    {
        await _auth.Register(user);
        return CreatedAtAction("registered", "registered");
    }

    // Login af bruger
    [HttpPost("/login")]
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _auth.Login(dto);
        if (!result.Contains("Invalid"))
        {
            return Ok(result);
        }

        return Unauthorized();
    }
}