using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Persistence;

// DbContext for AuthenticationService, håndterer kun brugere
public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}