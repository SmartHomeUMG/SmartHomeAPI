using Microsoft.EntityFrameworkCore;
using smartBuilding.Models;

namespace smartBuilding;
public class SmartBuildingDb : DbContext
{
    public SmartBuildingDb(DbContextOptions<SmartBuildingDb> options) : base(options) { }
    public virtual DbSet<HomeTemperature> Temperatures { get; set; }
    public virtual DbSet<Householders> Homeholders {get;set;}
    public virtual DbSet<HomePresences> HomePresences {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HomePresences>().HasOne(hp => hp.Householder).WithMany(hh => hh.HomePresences);
    }
    
}