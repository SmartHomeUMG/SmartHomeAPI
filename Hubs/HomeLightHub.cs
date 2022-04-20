using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using smartBuilding.Models;
using smartBuilding.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace smartBuilding.Hubs;

public class HomeLightHub : Hub
{
    private readonly IMemoryCache _memoryCache;
    private static int _RefrashDelay = 5000;

    public HomeLightHub(IMemoryCache _memCache)
    {
        _memoryCache = _memCache;
    }

    public async IAsyncEnumerable<HomeLight> CurrentLightStatus (CancellationToken stopToken)
    {
        var cacheKey = "lightStatus";
        while(! stopToken.IsCancellationRequested)
        {   
            _memoryCache.TryGetValue(cacheKey, out HomeLight homeLight);
            yield return homeLight;
            try
            {
                await Task.Delay(HomeLightHub._RefrashDelay,stopToken);
            }
            catch(Exception)
            {
                yield break;
            }
        }
    }
}