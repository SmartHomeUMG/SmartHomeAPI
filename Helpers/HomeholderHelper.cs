using smartBuilding;
using smartBuilding.Models;

namespace smartBuilding.Helpers;

public static class HomeholderHelper
{
    public static bool IsHomeHolder(SmartBuildingDb db, string code) => db.Homeholders.Where(hh => hh.IdentifyCode == code).FirstOrDefault() != null;
}