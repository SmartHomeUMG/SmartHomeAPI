using Microsoft.EntityFrameworkCore;
using smartBuilding.Models;

namespace smartBuilding;
class SmartBuildingDb : DbContext
{
    public SmartBuildingDb(DbContextOptions options) : base(options) { }
    public DbSet<HomeTemperature> Temperatures { get; set; }
}