using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Interfaces;

// DbContext interface til AuthenticationService
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
}