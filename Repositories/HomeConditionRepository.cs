using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;

namespace smartBuilding.Repositories;

public interface IHomeConditionRepository
{
    public Task<IEnumerable<HomeTemperature>> GetTemperatures();
    public Task<IEnumerable<HomeTemperature>> GetTemperatures(DateTime start, DateTime stop);
    public  Task AddTemperature(int temperatureC);
    public Task<HomeTemperature> GetRecentTemperature();

    public Task<bool> SaveChangesAsync();
}

public class HomeConditionRepository : IHomeConditionRepository
{
    private readonly SmartBuildingDb _context;
    public HomeConditionRepository(SmartBuildingDb contextDb)
    {
        _context = contextDb;
    }
    
    public async Task<IEnumerable<HomeTemperature>> GetTemperatures() => await _context.Temperatures.ToListAsync();
    //app.MapGet("/temperatures/period/{start:datetime},{stop:datetime}", async(SmartBuildingDb db, DateTime start, DateTime stop) => await db.Temperatures.Where(t => t.MeasureDate < stop && t.MeasureDate > start).ToListAsync());

    public async Task<IEnumerable<HomeTemperature>> GetTemperatures(DateTime start, DateTime stop) => await _context.Temperatures.Where(t => t.MeasureDate >= start && t.MeasureDate <= stop).ToListAsync();

    public async Task AddTemperature(int temperatureC)
    {
        HomeTemperature ht = new HomeTemperature(){
            Id = new smartBuilding.Helpers.IDGeneratorHelper().generateID(),
            TemperatureC = temperatureC,
            MeasureDate = DateTime.Now,
        };
        await _context.Temperatures.AddAsync(ht);
    }

    public async Task<HomeTemperature> GetRecentTemperature() => await _context.Temperatures.OrderBy(ht => ht.MeasureDate).LastOrDefaultAsync();
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0; 
    }
}