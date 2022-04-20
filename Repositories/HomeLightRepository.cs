using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;

namespace  smartBuilding.Repositories;


public interface IHomeLightRepository
{
    public Task AddLightStatus(HomeLight homeLight);
    public Task<HomeLight> GetRecentLightStatus();
    public  Task<IEnumerable<HomeLight>> GetAll();
}

public class HomeLightRepository : BaseRepository, IHomeLightRepository
{

    public static HomeLight recentStatus = new HomeLight(){TurnedOn = false, Date = DateTime.Now};
    public HomeLightRepository(SmartBuildingDb db) : base(db){}

    public async Task<IEnumerable<HomeLight>> GetAll() => await _context.HomeLights.ToListAsync();
    public async Task AddLightStatus(HomeLight homeLight)
    {
        await _context.HomeLights.AddAsync(homeLight);
        HomeLightRepository.recentStatus = homeLight;
    }

    public async Task<HomeLight> GetRecentLightStatus() => await _context.HomeLights.OrderBy(hl => hl.Date).LastOrDefaultAsync();
}