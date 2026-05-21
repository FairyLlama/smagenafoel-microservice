using System.Text.Json;
using BlazorLogin.Components;
using BlazorLogin.Interfaces;
using BlazorLogin.Services;

var builder = WebApplication.CreateBuilder(args);

// Tilføj services til containeren.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Opret HTTP klient med URI fra appsettings ellers brug en standard http url: 'localhost:8084'
builder.Services.AddHttpClient(
    "Authentication", client => 
    client.BaseAddress = new Uri(builder.Configuration["AuthAddress"] ?? "http://localhost:8084")
);
// Service registrering af Authentication service
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Konfigurer HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run("http://0.0.0.0:8085");
