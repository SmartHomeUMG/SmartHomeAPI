using Microsoft.AspNetCore.Mvc;

using smartBuilding.Repositories;
using smartBuilding.Models;
using Microsoft.Extensions.Caching.Memory;

namespace smartBuilding.Controllers;

[ApiController]
public class HomeConditionController : ControllerBase
{
   

    private readonly ILogger<HomeConditionController> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IHomeConditionRepository _iHomeConditionRepository;

    public HomeConditionController(ILogger<HomeConditionController> logger, IMemoryCache _memCache, IHomeConditionRepository homeRepository)
    {
        _logger = logger;
        _iHomeConditionRepository = homeRepository;
        _memoryCache = _memCache;
    }
    
    [HttpGet]
    [Route("[controller]/")]
    public async Task<IEnumerable<HomeTemperature>> Get()
    {
        return await _iHomeConditionRepository.GetTemperatures();
    }

    //localhost:5108/HomeCondition/GetTemperatureDuringTime?start=2022-04-01&&stop=2022-04-14
    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IEnumerable<HomeTemperature>> GetTemperatureDuringTime(DateTime start, DateTime stop) => 
    await _iHomeConditionRepository.GetTemperatures(start,stop); 

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<ActionResult<int>> RecentTemperature()
    {
        var cacheKey = "recentTemperature";
        if(! _memoryCache.TryGetValue(cacheKey, out int ht))
        {
            ht = (await _iHomeConditionRepository.GetRecentTemperature()).TemperatureC;
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, ht,cacheExpiryOptions);
        }
        return Ok(ht);
    }
        

    
    [HttpGet]
    [Route("[controller]/Temperature/Add")]
    public async Task<IActionResult> AddTemperature(int temperatureC)
    {
        var cacheKey = "recentTemperature";
        await _iHomeConditionRepository.AddTemperature(temperatureC);

        if (await _iHomeConditionRepository.SaveChangesAsync())
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, temperatureC,cacheExpiryOptions);
            return Ok(temperatureC);
        }
       
        return NoContent();
    }
}