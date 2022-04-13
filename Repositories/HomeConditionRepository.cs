using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;

namespace smartBuilding.Repositories;

public interface IHomeConditionRepository
{
    public Task<IEnumerable<HomeTemperature>> GetTemperatures();
    public Task AddTemperature(HomeTemperature temperatureC);

    public Task<bool> SaveChangesAsync();
}

public class HomeConditionRepository : IHomeConditionRepository
{
    private readonly SmartBuildingDb _context;
    public static HomeTemperature Recent;

    public HomeConditionRepository(SmartBuildingDb contextDb)
    {
        _context = contextDb;
    }
    
    public async Task<IEnumerable<HomeTemperature>> GetTemperatures() => await _context.Temperatures.ToListAsync();

    public async Task AddTemperature(HomeTemperature temperatureC)
    {
        temperatureC.MeasureDate = DateTime.Now;
        await _context.Temperatures.AddAsync(temperatureC);
        HomeConditionRepository.Recent = temperatureC;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0; 
    }
}