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

    
    public Task AddHumidity(int humidity);
    public Task<IEnumerable<HomeHumidity>> GetHumidites();
     public Task<HomeHumidity> GetRecentHumidity();
    public Task<IEnumerable<HomeHumidity>> GetHumidites(DateTime start, DateTime stop);
    
    public Task AddWaterLevel(int level);

    public Task<HomeWaterLevel> GetRecentWaterLevel();
    public Task<IEnumerable<HomeWaterLevel>> GetWaterLevels();
     public Task<IEnumerable<HomeWaterLevel>> GetWaterLevels(DateTime start, DateTime stop);

    public Task<bool> SaveChangesAsync();
}

public class HomeConditionRepository : IHomeConditionRepository
{
    private readonly SmartBuildingDb _context;
    public HomeConditionRepository(SmartBuildingDb contextDb)
    {
        _context = contextDb;
    }
    
    //Temperatures section:
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
    
    //Humidity section:
    public async Task AddHumidity(int humidity){
        HomeHumidity hh = new HomeHumidity(){
            Id = new smartBuilding.Helpers.IDGeneratorHelper().generateID(),
            Humidity = humidity,
            MeasureDate = DateTime.Now,
        };
        
        await _context.Humidities.AddAsync(hh);        
    }

    public async Task<HomeHumidity> GetRecentHumidity() => await _context.Humidities.OrderBy(h => h.MeasureDate).LastOrDefaultAsync();

    public async Task<IEnumerable<HomeHumidity>> GetHumidites() => await _context.Humidities.ToListAsync();
    
    public async Task<IEnumerable<HomeHumidity>> GetHumidites(DateTime start, DateTime stop) => await _context.Humidities.Where(h => h.MeasureDate >= start && h.MeasureDate <= stop).ToListAsync();
    //Water level section:
    public async Task AddWaterLevel(int level){
        HomeWaterLevel hwl = new HomeWaterLevel(){
            Id = new smartBuilding.Helpers.IDGeneratorHelper().generateID(),
            WaterLevel  = level,
            MeasureDate = DateTime.Now
        };
        await _context.WaterLevels.AddAsync(hwl);
    }

    public async Task<HomeWaterLevel> GetRecentWaterLevel() => await _context.WaterLevels.OrderBy(l => l.MeasureDate).LastOrDefaultAsync();
    public async Task<IEnumerable<HomeWaterLevel>> GetWaterLevels() => await _context.WaterLevels.ToListAsync();
    
    public async Task<IEnumerable<HomeWaterLevel>> GetWaterLevels(DateTime start, DateTime stop) => await _context.WaterLevels.Where(l => l.MeasureDate >= start && l.MeasureDate <= stop).ToListAsync();

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0; 
    }
}