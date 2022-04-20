using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using smartBuilding;
using smartBuilding.Models;

namespace  smartBuilding.Repositories;

public class BaseRepository
{
    protected SmartBuildingDb _context;
    public BaseRepository(SmartBuildingDb db)
    {
        _context = db;
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0; 
    }
}