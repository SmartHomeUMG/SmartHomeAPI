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
    [Route("[controller]/Temperature")]
    public async Task<IEnumerable<HomeTemperature>> Get()
    {
        return await _iHomeConditionRepository.GetTemperatures();
    }

    //localhost:5108/HomeCondition/DuringTime?start=2022-04-01&&stop=2022-04-14
    [HttpGet]
    [Route("[controller]/Temperature/DuringTime")]
    public async Task<IEnumerable<HomeTemperature>> GetTemperatureDuringTime(DateTime start, DateTime stop) => 
    await _iHomeConditionRepository.GetTemperatures(start,stop); 

    [HttpGet]
    [Route("[controller]/Temperature/Recent")]
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
    [Route("[controller]/Conditions/Add")]
    public async Task<IActionResult> AddConditions(int temperatureC= -100,int humidity= -100)
    {
        var cacheKeyTemperature = "recentTemperature";
        var cacheKeyHumidity = "recentHumidity";
        await _iHomeConditionRepository.AddTemperature(temperatureC);
        await _iHomeConditionRepository.AddHumidity(humidity);

        if (await _iHomeConditionRepository.SaveChangesAsync())
        {
            var cacheTemperatureExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            var cacheHumidityExpirtyOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };

            _memoryCache.Set(cacheKeyTemperature, temperatureC,cacheTemperatureExpiryOptions);
            _memoryCache.Set(cacheKeyHumidity, humidity,cacheHumidityExpirtyOptions);
            
            (int temperatureC, int humidity) value = (temperatureC, humidity);
            return base.Ok(value);
        }
       
        return NoContent();
    }

    [HttpGet]
    [Route("[controller]/Humidity")]
    public async Task<IEnumerable<HomeHumidity>> GetHumidities()
    {
        return await _iHomeConditionRepository.GetHumidites();
    }

    [HttpGet]
    [Route("[controller]/Humidity/Recent")]
    public async Task<ActionResult<int>> RecentHumidity()
    {
        var cacheKey = "recentHumidity";
        if(! _memoryCache.TryGetValue(cacheKey, out int ht))
        {
            ht = (await _iHomeConditionRepository.GetRecentHumidity()).Humidity;
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
    [Route("[controller]/Humidity/DuringTime")]
    public async Task<IEnumerable<HomeHumidity>> GetHumiditiesDuringTime(DateTime start, DateTime stop) => await _iHomeConditionRepository.GetHumidites(start,stop);

    [HttpGet]
    [Route("[controller]/WaterLevel/Add")]
    public async Task<IActionResult> AddWaterLevel(int level)
    {
        var cacheKey = "recentWaterLevel";
        await _iHomeConditionRepository.AddWaterLevel(level);

        if (await _iHomeConditionRepository.SaveChangesAsync())
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(cacheKey, level,cacheExpiryOptions);
            return Ok(level);
        }
       
        return NoContent();
    }


    [HttpGet]
    [Route("[controller]/WaterLevel/DuringTime")]
    public async Task<IEnumerable<HomeWaterLevel>> GetWaterLevelDuringTime(DateTime start, DateTime stop) => await _iHomeConditionRepository.GetWaterLevels(start,stop); 

}