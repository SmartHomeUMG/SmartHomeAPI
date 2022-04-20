using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using smartBuilding.Models;
using smartBuilding.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace smartBuilding.Hubs;

public class HomeConditionHub : Hub
{
    private readonly IMemoryCache _memoryCache;
    public HomeConditionHub(IMemoryCache _memCache)
    {
        _memoryCache = _memCache;
    }
    private static int _RefrashDelay = 5000;
    public async IAsyncEnumerable<int> RecentTemperature(CancellationToken stopToken)
    {
        var cacheKey = "recentTemperature";
        while(! stopToken.IsCancellationRequested)
        {
            _memoryCache.TryGetValue(cacheKey, out int recentTemperature);
            yield return recentTemperature;
            try
            {
                await Task.Delay(HomeConditionHub._RefrashDelay,stopToken);
            }
            catch(Exception)
            {
                yield break;
            }
        }
    }
}