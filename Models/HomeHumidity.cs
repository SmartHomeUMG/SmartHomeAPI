using Microsoft.EntityFrameworkCore;

namespace smartBuilding.Models;

public class HomeHumidity
{

    public string Id {get;set;}
    private int _Humidity;
    public int Humidity
    {
        get
        {
            return _Humidity;
        }
        set
        {
            _Humidity = value;  
        }
    }
    public DateTime MeasureDate {get;set;}
}