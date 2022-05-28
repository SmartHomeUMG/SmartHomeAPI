using Microsoft.EntityFrameworkCore;

namespace smartBuilding.Models;

public class HomeWaterLevel
{

    public string Id {get;set;}
    private int _WaterLevel;
    public int WaterLevel
    {
        get
        {
            return _WaterLevel;
        }
        set
        {
            _WaterLevel = value;  
        }
    }
    public DateTime MeasureDate {get;set;}
}