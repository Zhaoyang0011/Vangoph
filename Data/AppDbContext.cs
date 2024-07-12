using Microsoft.EntityFrameworkCore;
using Vangoph.Models;

namespace Vangoph.Data;

public class AppDbContext : DbContext
{
    public DbSet<Image>? Images { get; set; }
    public DbSet<User>? Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}