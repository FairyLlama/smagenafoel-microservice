namespace BlazorLogin.Models;

// Basic DTO til login respons
public record LoginResponse(int StatusCode, string Jwt);