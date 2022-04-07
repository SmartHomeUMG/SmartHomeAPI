using Microsoft.EntityFrameworkCore;
using smartBuilding.Models;

namespace smartBuilding;
class SmartBuildingDb : DbContext
{
    public SmartBuildingDb(DbContextOptions options) : base(options) { }
    public DbSet<HomeTemperature> Temperatures { get; set; }
    public DbSet<Householders> Homeholders {get;set;}
    public DbSet<HomePresences> HomePresences {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HomePresences>().HasOne(hp => hp.Householder);
    }
    
}