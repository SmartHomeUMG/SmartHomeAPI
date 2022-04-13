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

    [HttpGet]
    [Route("[controller]/[action]")]
    public HomeTemperature RecentTemperature() => HomeConditionRepository.Recent;

    [HttpPost]
    [Route("[controller]")]
    public async Task<IActionResult> Post([FromBody] HomeTemperature temperatureC)
    {
        await _iHomeConditionRepository.AddTemperature(temperatureC);
        return await _iHomeConditionRepository.SaveChangesAsync() ? Ok(temperatureC) : BadRequest();
    }
}