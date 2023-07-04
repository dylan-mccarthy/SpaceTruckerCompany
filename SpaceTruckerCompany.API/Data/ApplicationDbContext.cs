using Microsoft.EntityFrameworkCore;
using SpaceTruckerCompany.API.Models;

namespace SpaceTruckerCompany.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<SpaceShip> SpaceShips { get; set; } = null!;
    public DbSet<SpaceShipEntry> SpaceShipEntries { get; set; } = null!;
    public DbSet<TradeItem> TradeItems { get; set; } = null!;
    public DbSet<TradeItemEntry> TradeItemEntries { get; set; } = null!;
    public DbSet<SpaceStation> SpaceStations { get; set; } = null!;
    public DbSet<SpaceShipRoute> SpaceShipRoutes { get; set; } = null!;

}
