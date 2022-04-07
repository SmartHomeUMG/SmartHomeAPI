namespace smartBuilding.Models;

public class HomePresences
{
    public int Id {get;set;}
    public bool isPresent {get;set;}
    public DateTime CheckIn {get;set;}
    public Householders Householder {get;set;}
    
}