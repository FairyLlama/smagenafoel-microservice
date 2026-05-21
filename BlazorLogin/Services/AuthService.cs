using System.Net;
using System.Text;
using System.Text.Json;
using BlazorLogin.Interfaces;
using BlazorLogin.Models;

namespace BlazorLogin.Services;

// Implementering af IAuthService
public class AuthService(IHttpClientFactory httpFactory) : IAuthService
{
    // Opret HTTP klient fra app-service (Program.cs)
    private readonly HttpClient _http = httpFactory.CreateClient("Authentication");

    // Overskriv interface metode RegisterUserAsync
    public async Task<HttpStatusCode> RegisterUserAsync(UserDto dto)
    {
        // Klargør HTTP-request body til Auth POST request
        var request = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

        // Send POST request til registrerings endpointet og returner status koden
        var response = await _http.PostAsync("/register", request);
        return response.StatusCode;
    }

    // Overskriv interface metode LoginUserAsync
    public async Task<LoginResponse> LoginUserAsync(LoginDto dto)
    {
        // Klargør HTTP-request body til Auth POST request
        var request = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

        // Send POST request til login endpointet
        var response = await _http.PostAsync("/login", request);
        var status = response.StatusCode;
        var token = await response.Content.ReadAsStringAsync();

        return new((int)status, token);
    }
}
