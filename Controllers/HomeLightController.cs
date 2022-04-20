using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

using smartBuilding.Repositories;
using smartBuilding.Models;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Caching.Memory;

namespace smartBuilding.Controllers;

[ApiController]
public class HomeLightController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHomeLightRepository _iHomeLightRepo;

    public HomeLightController(IHomeLightRepository _ihomeRepo, IMemoryCache _memCache)
    {
        _iHomeLightRepo = _ihomeRepo;
        _memoryCache = _memCache;
    }

    [HttpGet()]
    [Route("[controller]/GetHistory")]
    public async Task<IActionResult> GetHistory()
    {
        
        return Ok(await _iHomeLightRepo.GetAll());
    }

    [HttpGet()]
    [Route("[controller]/GetLightStatus")]
    public async Task<IActionResult> GetLightStatus()
    {
        var cacheKey = "lightStatus";
        if(!_memoryCache.TryGetValue(cacheKey, out HomeLight homeLight))
        {
            homeLight = await _iHomeLightRepo.GetRecentLightStatus();
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, homeLight,cacheExpiryOptions);
        }
        return Ok(homeLight);
    }

    [HttpPost()]
    [Route("[controller]/ChangeLightStatus")]
    public async Task<IActionResult> ChangeLightStatus([FromBody]HomeLight homeLight)
    {
        homeLight.Date = DateTime.Now;
        await _iHomeLightRepo.AddLightStatus(homeLight);
        
        bool result = await ((BaseRepository)_iHomeLightRepo).SaveChangesAsync();

        //update cache
        if(result)
        {
            var cacheKey = "lightStatus";
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, homeLight,cacheExpiryOptions);
        }
        
        return result == true ? Ok() : BadRequest();
    }
}