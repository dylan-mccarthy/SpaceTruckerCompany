using Microsoft.EntityFrameworkCore;

namespace SpaceTruckerCompany.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; } = null!;
}
