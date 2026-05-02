using AuthenticationService.Interfaces;
using AuthenticationService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService;

// Konfiguration af AuthenticationService
public class AuthConfiguration
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            // b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
}