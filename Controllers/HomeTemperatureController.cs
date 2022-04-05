using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using SmartBuilding.Models;
namespace smartBuilding.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeTemperatureController : ControllerBase
{

    [HttpGet("recent")]
    public int GetTemperature(){
        return Random.Shared.Next(20,55);
    }

    [HttpGet("timePeriodTemperatures")]
    public IEnumerable<int> GetTemperaturePeriod(DateTime start, DateTime end) => 
        Enumerable.Range(1, 5).Select(index => Random.Shared.Next(20, 55)).ToArray();
}