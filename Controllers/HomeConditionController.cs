using Microsoft.AspNetCore.Mvc;

using smartBuilding.Repositories;
using smartBuilding.Models;

namespace smartBuilding.Controllers;

[ApiController]
public class HomeConditionController : ControllerBase
{
   

    private readonly ILogger<HomeConditionController> _logger;
    private readonly IHomeConditionRepository _iHomeConditionRepository;

    public HomeConditionController(ILogger<HomeConditionController> logger, IHomeConditionRepository homeRepository)
    {
        _logger = logger;
        _iHomeConditionRepository = homeRepository;
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
    public HomeTemperature RecentTemperature() => HomeConditionRepository.Recent;

    [HttpPost]
    [Route("[controller]/Temperature")]
    public async Task<IActionResult> Post([FromBody] HomeTemperature temperatureC)
    {
        await _iHomeConditionRepository.AddTemperature(temperatureC);
        return await _iHomeConditionRepository.SaveChangesAsync() ? Ok(temperatureC) : BadRequest();
    }
}