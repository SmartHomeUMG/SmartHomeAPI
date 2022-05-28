using Microsoft.EntityFrameworkCore;
using smartBuilding.Models;

namespace smartBuilding;
public class SmartBuildingDb : DbContext
{
    public SmartBuildingDb(DbContextOptions<SmartBuildingDb> options) : base(options) { }
    public virtual DbSet<HomeTemperature> Temperatures { get; set; }
    public virtual DbSet<HomeHumidity> Humidities {get;set;}
    public virtual DbSet<HomeWaterLevel> WaterLevels {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //setting primary keys:

        modelBuilder.Entity<HomeTemperature>()
        .HasKey(hc => hc.Id)
        .HasName("PrimaryKey_HomeTemperatureId");
        
        modelBuilder.Entity<HomeHumidity>()
        .HasKey(hc => hc.Id)
        .HasName("PrimaryKey_HomeHumidityId");

        modelBuilder.Entity<HomeWaterLevel>()
        .HasKey(hc => hc.Id)
        .HasName("PrimaryKey_HomeWaterLevelId");
        //setting relationships:
       
    }
    
}