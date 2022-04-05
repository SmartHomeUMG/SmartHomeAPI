using Microsoft.EntityFrameworkCore;

namespace smartBuilding.Models;

public class HomeTemperature
{
    public int Id {get;set;}
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
           
    private DateTime _MeasureDate = DateTime.Now;
    public DateTime MeasureDate {get;set;}
}