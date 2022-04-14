using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using smartBuilding.Models;
using smartBuilding.Repositories;

namespace smartBuilding.Hubs;

public class HomeConditionHub : Hub
{
    private static int _RefrashDelay = 5000;
    public async IAsyncEnumerable<HomeTemperature> RecentTemperature(CancellationToken stopToken)
    {
        while(! stopToken.IsCancellationRequested)
        {
            yield return HomeConditionRepository.Recent;
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