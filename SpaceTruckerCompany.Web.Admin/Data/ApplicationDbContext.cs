using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceTruckerCompany.Web.Admin.Models;

namespace SpaceTruckerCompany.Web.Admin.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<SpaceShip> SpaceShips { get; set; } = null!;
    public DbSet<SpaceShipEntry> SpaceShipEntries { get; set; } = null!;
    public DbSet<TradeItem> TradeItems { get; set; } = null!;
    public DbSet<TradeItemEntry> TradeItemEntries { get; set; } = null!;
    public DbSet<SpaceStation> SpaceStations { get; set; } = null!;
    public DbSet<SpaceShipRoute> SpaceShipRoutes { get; set; } = null!;
}
