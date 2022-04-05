using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartBuilding.Models;

namespace smartBuilding.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeVisitorController : ControllerBase
{
    private static List<HomeVisitor> visitors = new List<HomeVisitor>(){
        new HomeVisitor()

    };
    private readonly ILogger<HomeVisitorController> _logger;

    public HomeVisitorController(ILogger<HomeVisitorController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "")]
    public HomeVisitor Get()
    {
        return new HomeVisitor(){
            Id = 1
        };
    }
}
