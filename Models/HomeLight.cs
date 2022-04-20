namespace smartBuilding.Models;

public class HomeLight
{
    
    public string Id = new smartBuilding.Helpers.IDGeneratorHelper().generateID();
    public bool TurnedOn {get;set;}
    public DateTime Date {get; set;}
}