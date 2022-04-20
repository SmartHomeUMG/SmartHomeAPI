using Microsoft.EntityFrameworkCore;

namespace smartBuilding.Models;

public class HomeTemperature
{

    public string Id {get;set;}
    private int _TemperatureC;
    public int TemperatureC
    {
        get
        {
            return _TemperatureC;
        }
        set
        {
            _TemperatureC = value;  
        }
    }
    public DateTime MeasureDate {get;set;}
}